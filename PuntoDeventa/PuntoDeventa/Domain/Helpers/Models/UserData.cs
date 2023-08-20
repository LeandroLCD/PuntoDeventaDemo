using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuntoDeventa.Domain.Helpers.Models
{
    public class UserData
    {
        public string LocalId { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string IdToken { get; set; }
        public DateTime DateLogin { get; set; }
        public bool IsAuthValid => DateLogin > DateTime.Now.AddHours(12) ? false : true;
    }
}
