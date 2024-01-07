﻿namespace PuntoDeVenta.Maui.Core.LocalData.DataBase.Entities.CatalogueClient
{
    using PuntoDeVenta.Maui.Core.LocalData.DataBase.SQL;
    using System;
    using System.Collections.Generic;
    public class ClientEntity
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string Rut { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public int Rubro { get; set; }

        public DateTime BirbBirthDate { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastAttention { get; set; }

        public string CommuneName { get; set; }

        [ForeignKey(typeof(SalesRoutesEntity))]
        public string RouteId { get; set; }

        [OneToMany(CascadeOperation.All)]
        public List<BranchOfficesEntity> BranchOffices { get; set; }

        [OneToMany(CascadeOperation.All)]
        public List<EconomicActivitiesEntity> EconomicActivities { get; set; }
    }
}
