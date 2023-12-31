﻿using Newtonsoft.Json;
using PuntoDeventa.UI.CatalogueClient.Model;
using System.Collections.Generic;

namespace PuntoDeventa.Data.DTO
{
    public class EcommerceDTO
    {
        [JsonProperty("apiKey")]
        public string ApiKey { get; set; }


        [JsonProperty("iva")]
        public double Iva { get; set; }

        [JsonProperty("distributorCode")]
        public string DistributorCode { get; set; }

        [JsonProperty("rut")]
        public string Rut { get; set; }

        [JsonProperty("razonSocial")]
        public string Name { get; set; }

        [JsonProperty("telefono")]
        public string Phone { get; set; }

        [JsonProperty("direccion")]
        public string Address { get; set; }

        [JsonProperty("cdgSIISucur")]
        public string CdgSIISucur { get; set; }

        [JsonProperty("glosaDescriptiva")]
        public string DescriptiveGloss { get; set; }

        [JsonProperty("direccionRegional")]
        public string RegionalDirection { get; set; }

        [JsonProperty("comuna")]
        public string Commune { get; set; }

        [JsonProperty("nombreFantasia")]
        public string FantasyName { get; set; }

        [JsonProperty("correo")]
        public string Email { get; set; }

        [JsonProperty("sucursales")]
        public List<BranchOffices> BranchOffices { get; set; }

        [JsonProperty("actividades")]
        public List<EconomicActivities> EconomicActivities { get; set; }

        public override string ToString()
        {
            return $"Empresa:{Name}, apiKey: {ApiKey}";
        }
    }
}
