﻿using System.ComponentModel.DataAnnotations;

namespace PuntoDeventa.UI.CatalogueClient.Model
{
    public class Commune
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido.")]
        public string Name { get; set; }

        public string RegionId { get; set; }
    }
}
