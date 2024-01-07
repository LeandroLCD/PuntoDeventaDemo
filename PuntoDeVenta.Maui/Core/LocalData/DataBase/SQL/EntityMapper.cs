using Microsoft.Data.Sqlite;
using PuntoDeVenta.Maui.Domain.Helpers;
using System.Reflection;

namespace PuntoDeVenta.Maui.Core.LocalData.DataBase.SQL
{
    internal static class EntityMapper
    {
        /// <summary>
        /// Crea sino existe la tabla en la base de datos, y mapea las relaciones si existen en la tabla.
        /// </summary>
        /// <typeparam name="T">Es type de la la entidad.</typeparam>
        public static void Map<T>(this SqliteConnection connection)
        {
            var tableName = GetTableName<T>();
            var properties = typeof(T).GetProperties();

            // Crea la instrucción para crear la tabla
            var createTableSql = $"CREATE TABLE IF NOT EXISTS {tableName} ({GetPrimaryKeyDefinition<T>()}, {GetPropertyDefinitions<T>()})";
            using var command = connection.CreateCommand();
            command.CommandText = createTableSql;
            command.ExecuteNonQuery();

            // Mapea las relaciones
            foreach (var property in properties)
            {
                if (Attribute.IsDefined(property, typeof(OneToManyAttribute)))
                {
                    MapForeignKey<T>(connection, property);
                }
            }
        }

        

        private static void MapForeignKey<T>(SqliteConnection connection, PropertyInfo property)
        {
            // Obtén información sobre la relación ForeignKey
            var foreignKey = GetForeignKeyColumnName<T>(property);
            if (foreignKey is null)
                return;
            // Crea la instrucción para agregar la clave foránea a la tabla
            var addForeignKeySql = $"ALTER TABLE {GetTableName<T>()} ADD COLUMN {foreignKey} INTEGER";
            using var command = connection.CreateCommand();
            command.CommandText = addForeignKeySql;
            command.ExecuteNonQuery();
        }

        private static string GetPrimaryKeyDefinition<T>()
        {
            var primaryKeyProperty = typeof(T).GetProperties().FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(PrimaryKeyAttribute)));
            return primaryKeyProperty != null ? $"{primaryKeyProperty.Name} {primaryKeyProperty.GetType().Name.ToUpper()} PRIMARY KEY" : "Id INTEGER PRIMARY KEY AUTOINCREMENT";
        }

        private static string GetPropertyDefinitions<T>()
        {
            var properties = typeof(T).GetProperties().Where(prop => !Attribute.IsDefined(prop, typeof(OneToManyAttribute)) && !Attribute.IsDefined(prop, typeof(PrimaryKeyAttribute)));
            return string.Join(", ", properties.Select(GetPropertyDefinition));
        }

        private static string GetPropertyDefinition(PropertyInfo property)
        {
            var propertyName = property.Name;
            var propertyType = GetSqliteType(property.PropertyType);
            return $"{propertyName} {propertyType}";
        }

        private static string GetForeignKeyColumnName<T>(PropertyInfo property)
        {
            var foreignKeyAttribute = (ForeignKeyAttribute)property.GetCustomAttribute(typeof(ForeignKeyAttribute));
            return foreignKeyAttribute is null ? null : foreignKeyAttribute.ReferenceType.Name + "Id";
        }

        private static string GetSqliteType(Type type)
        {
            return Type.GetTypeCode(type) switch
            {
                TypeCode.Boolean => "INTEGER",
                TypeCode.Int32 or TypeCode.Int64 => "INTEGER",
                TypeCode.Single or TypeCode.Double => "REAL",
                TypeCode.String => "TEXT",
                TypeCode.DateTime => "DATETIME",
                _ => throw new NotSupportedException($"Tipo no soportado: {type}"),
            };
        }

        private static string GetTableName<T>()
        {
            return typeof(T).Name;
        }
        
    }

}
