﻿using System;

namespace PuntoDeventa.Domain.Models
{
    public class UserData
    {
        public string LocalId { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string IdToken { get; set; }
        public DateTime DateLogin { get; set; }
        public bool IsAuthValid => DateTime.Now > DateLogin.AddHours(12) ? false : true;
    }
}
