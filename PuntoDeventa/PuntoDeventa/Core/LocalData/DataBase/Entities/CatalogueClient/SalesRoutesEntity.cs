using PuntoDeventa.UI.CatalogueClient.Model;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace PuntoDeventa.Core.LocalData.DataBase.Entities.CatalogueClient
{
    public class SalesRoutesEntity
    {
        [PrimaryKey, Unique]
        public string Id { get; set; }

        public string Name { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<ClientEntity> Clients { get; set; }
    }
}
