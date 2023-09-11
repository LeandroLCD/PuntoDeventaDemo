using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace PuntoDeventa.Core.LocalData.DataBase.Entities
{
    public class CategoryEntity
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<ProductEntity> Products { get; set; }
    }
}
