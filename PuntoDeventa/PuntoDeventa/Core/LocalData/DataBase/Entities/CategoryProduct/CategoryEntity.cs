using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace PuntoDeventa.Core.LocalData.DataBase.Entities.CatalogueClient
{
    public class CategoryEntity
    {
        [PrimaryKey, Unique]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<ProductEntity> Products { get; set; }
    }
}
