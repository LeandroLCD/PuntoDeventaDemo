using System;
using System.Text;

namespace PuntoDeventa.Domain.Helpers
{
    public static class Factory
    {
        public static string GenerateId()
        {
            // Obtener la hora actual
            var currentTime = DateTime.Now;

            // Obtener el identificador único del dispositivo
            var device = Xamarin.Essentials.DeviceInfo.Model;

            // Crear un objeto anónimo con las propiedades "time" y "device"
            var data = new
            {
                time = currentTime,
                device = device
            };

            // Serializar el objeto en formato JSON
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            // Convertir la cadena JSON en un array de bytes
            var bytes = Encoding.UTF8.GetBytes(json);

            // Convertir los bytes en una cadena Base64
            var base64String = Convert.ToBase64String(bytes);

            return base64String;
        }
    }
}
