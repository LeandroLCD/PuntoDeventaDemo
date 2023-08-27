using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using System.Reflection;
using PuntoDeventa.Domain.Models;

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

    }

}
