

namespace PuntoDeVenta.Maui.Core.LocalData.DataBase.SQL
{
    using Microsoft.Data.Sqlite;
    using PuntoDeVenta.Maui.Domain.Helpers;
    using System.Linq;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Diagnostics;

    internal static class SqliteConnectionExtensions
    {
        public static void InsertOrRemplace<T>(this SqliteConnection connection, T model)
        {
            using var command = connection.CreateCommand();
            var tableName = GetTableName(model.GetType());

            // Se capturan las propiedades públicas de la entidad
            var properties = model.GetType().GetProperties();

            var primaryKeyProperty = properties.FirstOrDefault(prop => prop.GetCustomAttribute<PrimaryKeyAttribute>() != null);

            var primaryKeyValue = primaryKeyProperty?.GetValue(model);
            if (primaryKeyValue is null)
            {
                var key = GetPrimaryKeyValue(connection, model).ToString();
                primaryKeyProperty.SetValue(model, key);
            }


            // Excluye la propiedad de clave primaria si existe
            var nonPrimaryKeyProperties = properties.Where(prop => prop.GetCustomAttribute<OneToManyAttribute>() == null);

            // Crea la instrucción INSERT utilizando el nombre de la tabla y las propiedades del modelo
            command.CommandText = $"INSERT INTO {tableName} ({string.Join(", ", nonPrimaryKeyProperties.Select(prop => prop.Name))}) VALUES ({string.Join(", ", nonPrimaryKeyProperties.Select(prop => $"'{prop.GetValue(model)}'"))})";

            // Ejecuta la inserción
            command.ExecuteNonQuery();
        }

        public static void InsertOrReplaceWithChildren<T>(this SqliteConnection connection, T mainObject)
        {
            
            
                connection.InsertOrRemplace(mainObject);
           var primaryKeyValue = GetPrimaryKeyValue(connection, mainObject);
            
            // Iterar sobre las propiedades marcadas con OneToManyAttribute
            var includeProperties = typeof(T).GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(OneToManyAttribute)))
                .ToList();

            foreach (var includeProperty in includeProperties)
            {
                // Obtener la lista de objetos relacionados de la propiedad de navegación

                 if (includeProperty.GetValue(mainObject) is IEnumerable<object> relatedList)
                {
                    // Eliminar los registros existentes de la tabla de relación
                    var relatedType = includeProperty.PropertyType.GetGenericArguments().FirstOrDefault();
                    var tableName = GetTableName(relatedType);
                    var foreignKeyColumnName = GetForeignKeyColumnName(relatedType);
                    var primaryKeyColumnName = GetPrimaryKeyColumnName<T>();

                    var deleteRelatedSql = $"DELETE FROM {tableName} WHERE {foreignKeyColumnName} = '{primaryKeyValue}'";
                    using (var deleteCommand = connection.CreateCommand())
                    {
                        deleteCommand.CommandText = deleteRelatedSql;
                        deleteCommand.ExecuteNonQuery();
                    }

                    // Insertar los nuevos registros de la tabla de relación
                    foreach (var relatedObject in relatedList)
                    {
                        try
                        {
                            // Asignar la relación inversa
                            includeProperty.SetValue(relatedObject, mainObject); 


                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                        }


                        // Insertar el objeto relacionado y la relación
                        var propertyForeignKey = relatedObject.GetType().GetProperty(foreignKeyColumnName);
                        propertyForeignKey.SetValue(relatedObject, primaryKeyValue);
                        connection.InsertOrRemplace(relatedObject);

                    }
                }
            }
        }

        public static void Update<T>(this SqliteConnection connection, T model)
        {
            using var command = connection.CreateCommand();
            var tableName = GetTableName<T>();

            // Se capturan las propiedades públicas de la entidad
            var properties = typeof(T).GetProperties().Where(prop => prop.GetCustomAttribute<PrimaryKeyAttribute>() == null);

            // Crea la instrucción UPDATE utilizando el nombre de la tabla, las propiedades y el Id del modelo
            command.CommandText = $"UPDATE {tableName} SET {string.Join(", ", properties.Select(prop => $"{prop.Name} = '{prop.GetValue(model)}'"))} WHERE {GetPrimaryKeyColumnName<T>()} = {connection.GetPrimaryKeyValue(model)}";

            command.ExecuteNonQuery();
        }

        public static void Delete<T>(this SqliteConnection connection, T model)
        {
            using var command = connection.CreateCommand();
            var tableName = GetTableName<T>();

            // Obtén el Id de la entidad
            var id = connection.GetPrimaryKeyValue(model);

            // Crea la instrucción DELETE utilizando el nombre de la tabla y el Id del modelo
            command.CommandText = $"DELETE FROM {tableName} WHERE {GetPrimaryKeyColumnName<T>()} = '{id}'";

            command.ExecuteNonQuery();
        }

        public static T Get<T>(this SqliteConnection connection, object id)
        {
            using var command = connection.CreateCommand();
            var tableName = GetTableName<T>();

            command.CommandText = $"SELECT * FROM {tableName} WHERE {GetPrimaryKeyColumnName<T>()} = '{id}'";

            using var reader = command.ExecuteReader();
            return ReadSingleResult<T>(reader);
        }

        public static T GetWithChildren<T>(this SqliteConnection connection, object id)
        {
            var mainObject = connection.Get<T>(id);

            var includeProperty = typeof(T).GetProperties()
                .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(OneToManyAttribute)));

            if (includeProperty != null)
            {
                // Obtener el tipo de la propiedad de navegación
                var relatedType = includeProperty.PropertyType.GetGenericArguments().FirstOrDefault();

                // Construir el nombre de la tabla de relación
                var tableName = $"{GetTableName(relatedType)}";

                // Construir la instrucción SQL para obtener los objetos relacionados
                var selectRelatedSql = $"SELECT * FROM {tableName} WHERE {GetForeignKeyColumnName(relatedType)} = '{id}'";

                using var command = connection.CreateCommand();
                command.CommandText = selectRelatedSql;

                using var reader = command.ExecuteReader();
                var relatedList = Activator.CreateInstance(typeof(List<>).MakeGenericType(relatedType));

                while (reader.Read())
                {
                    var destinationItem = Activator.CreateInstance(relatedType);
                    var relatedObject = ReadSingleResult(reader, destinationItem);
                    relatedList.GetType().GetMethod("Add").Invoke(relatedList, new[] { relatedObject });
                }

                // Asignar la lista de objetos relacionados a la propiedad de navegación
                
                    includeProperty.SetValue(mainObject, relatedList);
            }

            return mainObject;
        }

        public static IEnumerable<T> GetAll<T>(this SqliteConnection connection)
        {
            var tableName = GetTableName<T>();

            var selectAllSql = $"SELECT * FROM {tableName}";

            var resultList = new List<T>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = selectAllSql;

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var result = ReadSingleResult<T>(reader);
                    resultList.Add(result);
                }
            }

            return resultList;
        }

        public static IEnumerable<T> GetAllWithChildren<T>(this SqliteConnection connection)
        {
            var resultList = new List<T>();

            var mainObjects = connection.GetAll<T>();

            // Iterar sobre cada objeto principal para incluir objetos relacionados
            foreach (var mainObject in mainObjects)
            {
                // Buscar propiedades marcadas con OneToManyAttribute
                var includeProperties = typeof(T).GetProperties()
                    .Where(prop => Attribute.IsDefined(prop, typeof(OneToManyAttribute)))
                    .ToList();

                // Incluir objetos relacionados para cada propiedad encontrada
                foreach (var includeProperty in includeProperties)
                {
                    // Obtener el tipo de la propiedad de navegación
                    var relatedType = includeProperty.PropertyType.GetGenericArguments().FirstOrDefault();

                    // Construir el nombre de la tabla de relación
                    var tableName = $"{GetTableName(relatedType)}";

                    // Obtener el valor de la clave primaria del objeto principal
                    var primaryKeyValue = mainObject.GetType().GetProperty(GetPrimaryKeyColumnName<T>()).GetValue(mainObject);

                    // Construir la instrucción SQL para obtener los objetos relacionados
                    var selectRelatedSql = $"SELECT * FROM {tableName} WHERE {GetForeignKeyColumnName(relatedType)} = '{primaryKeyValue}'";

                    using var command = connection.CreateCommand();
                    command.CommandText = selectRelatedSql;

                    using var reader = command.ExecuteReader();
                    var relatedList = Activator.CreateInstance(typeof(List<>).MakeGenericType(relatedType));

                    while (reader.Read())
                    {
                        var relatedObject = ReadSingleResult(reader, relatedType);
                        relatedList.GetType().GetMethod("Add").Invoke(relatedList, new[] { relatedObject }); ;
                    }
                    int count = (int)relatedList.GetType().GetProperty("Count").GetValue(relatedList);
                    if (count > 0)
                        includeProperty.SetValue(mainObject, relatedList);
                }

                resultList.Add(mainObject);
            }

            return resultList;
        }

        private static T ReadSingleResult<T>(SqliteDataReader reader)
        {
            // Crea una instancia del modelo y asigna los valores de la base de datos
            dynamic result = Activator.CreateInstance(typeof(T));
            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                var propName = prop.Name;
                var propType = prop.PropertyType;


                try
                {

                    if (reader[propName] != DBNull.Value)
                    {
                        prop.SetValue(result, Convert.ChangeType(reader[propName], propType));
                    }
                }
                catch
                {
                    continue;
                }
                
            }

            return result;
        }

        private static T ReadSingleResult<T>(SqliteDataReader reader, T obj)
        {
            // Crea una instancia del modelo y asigna los valores de la base de datos
                        
            var properties = obj?.GetType().GetProperties();

            foreach (var prop in properties)
            {
                var propName = prop.Name;
                var propType = prop.PropertyType;


                try
                {

                    if (reader[propName] != DBNull.Value)
                    {
                        prop.SetValue(obj, Convert.ChangeType(reader[propName], propType));
                    }
                }
                catch
                {
                    continue;
                }

            }

            return obj;
        }

        private static string GetForeignKeyColumnName<T>()
        {
            var foreignKeyProperty = typeof(T).GetProperties().FirstOrDefault(prop => prop.GetCustomAttribute<ForeignKeyAttribute>() != null);
            return foreignKeyProperty?.Name ?? ""; //TODO revisar que retorne el nombre de la propiedad de la relacion
        }

        private static string GetForeignKeyColumnName(Type obj)
        {
            var foreignKeyProperty = obj.GetProperties().FirstOrDefault(prop => prop.GetCustomAttribute<ForeignKeyAttribute>() != null);
            return foreignKeyProperty?.Name ?? "";
        }

        private static string GetTableName<T>()
        {
            // Obtiene el nombre de la tabla utilizando el nombre completo del tipo y eliminando el namespace
            return typeof(T).FullName?.Split('.').Last();
        }

        private static string GetTableName(Type obj)
        {
            // Obtiene el nombre de la tabla utilizando el nombre completo del tipo y eliminando el namespace
            return obj.FullName?.Split('.').Last();
        }

        private static string GetPrimaryKeyColumnName<T>()
        {
            var primaryKeyProperty = typeof(T).GetProperties().FirstOrDefault(prop => prop.GetCustomAttribute<PrimaryKeyAttribute>() != null);
            return primaryKeyProperty?.Name ?? "Id";
        }

        private static object GetPrimaryKeyValue<T>(this SqliteConnection connection, T model)
        {
            var primaryKeyProperty = model.GetType().GetProperties().FirstOrDefault(prop => prop.GetCustomAttribute<PrimaryKeyAttribute>() != null);
            if(primaryKeyProperty is null) {
                throw new Exception($"The atribute primary key is not present in {model.GetType().Name}");
            }
            
            var primaryKeyValue = primaryKeyProperty?.GetValue(model);

            // Si el valor de la clave primaria es nulo, generamos el valor según el tipo
            if (primaryKeyValue == null)
            {
                if (primaryKeyProperty.PropertyType == typeof(int))
                {
                    // Generar el siguiente número correspondiente en la base de datos para int
                    primaryKeyValue = GetNextIntPrimaryKeyValue<T>(connection);
                }
                else if (primaryKeyProperty.PropertyType == typeof(string))
                {
                    // Usar Factory.GenerateId para generar el valor para string
                    primaryKeyValue = Factory.GenerateId();
                }
                else
                {
                    // Otros tipos de clave primaria podrían requerir lógicas específicas
                    throw new InvalidOperationException("Tipo de clave primaria debe ser int o string.");
                }

                primaryKeyProperty.SetValue(model, primaryKeyValue);
            }

            return primaryKeyValue;
        }

        private static int GetNextIntPrimaryKeyValue<T>(SqliteConnection connection)
        {
            using var command = connection.CreateCommand();
            var tableName = GetTableName<T>();
            command.CommandText = $"SELECT MAX(Id) FROM {tableName}";
            var result = command.ExecuteScalar();
            return (result is int intValue) ? intValue + 1 : 1;
        }
    }
}

