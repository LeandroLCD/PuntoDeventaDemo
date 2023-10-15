using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using System.Reflection;
using PuntoDeventa.Domain.Models;
using Xamarin.Forms.Internals;
using System.Collections;
using System.Collections.ObjectModel;

namespace PuntoDeventa.Domain.Helpers
{
    public static class FuntionsExtention
    {
        public static List<ErrorMessage> DataAnotationsValid(this object obj)
        {
            var context = new ValidationContext(obj, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(obj, context, results, true);

            if (isValid)
            {
                return null;
            }

            var errores = results.Select(r => new ErrorMessage(r.MemberNames.FirstOrDefault(), r.ErrorMessage)).ToList();

            Type type = obj.GetType();

            errores.ForEach(e => {
                PropertyInfo property = type.GetProperty(e.Field);
                if (property.IsNotNull())
                {
                    var errorProperty = type.GetProperty(property.Name + "ErrorText");
                    if (errorProperty.IsNotNull())
                    {
                        errorProperty.SetValue(obj, e.Message);
                    }
                }
            });


            return errores;
        }

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        public static void CopyPropertiesFrom(this object destination, object source)
        {
            if (destination == null || source == null)
            {
                return;
            }

            var sourceProperties = source.GetType().GetProperties();
            var destinationType = destination.GetType();

            sourceProperties.ForEach(e =>
            {
                var destinationProperty = destinationType.GetProperty(e.Name);
                var value = e.GetValue(source, null);
                if (value.IsNotNull() && destinationProperty.IsNotNull() && destinationProperty.CanWrite)
                {
                    if (value is IEnumerable sourceCollection && destinationProperty.PropertyType.IsGenericType)
                    {
                        // Obtén el tipo genérico de la colección de destino
                        var destinationGenericType = destinationProperty.PropertyType.GetGenericArguments().FirstOrDefault();

                        if (destinationGenericType != null)
                        {
                            // Crea una instancia de la colección de destino
                            var destinationCollection = Activator.CreateInstance(typeof(List<>).MakeGenericType(destinationGenericType));

                            // Itera sobre los elementos de la colección de origen y cárgalos en la colección de destino
                            foreach (var sourceItem in sourceCollection)
                            {
                                var destinationItem = Activator.CreateInstance(destinationGenericType);
                                destinationItem.CopyPropertiesFrom(sourceItem);
                                destinationCollection.GetType().GetMethod("Add").Invoke(destinationCollection, new[] { destinationItem });
                            }

                            // Asigna la colección de destino al destino principal
                            destinationProperty.SetValue(destination, destinationCollection, null);
                        }
                    }
                    else
                    {
                        // Si no es una propiedad IEnumerable, simplemente establece el valor en la propiedad de destino
                        destinationProperty.SetValue(destination, value, null);
                    }
                }

            });
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
        {
            return new ObservableCollection<T>(list);
        }

        public static T Clone<T>(this T source) where T : class, new()
        {
            if (source == null)
                return null;

            // Creamos una nueva instancia del tipo T
            T clone = new T();

            clone.CopyPropertiesFrom(source);

            return clone;
        }
    }

}
