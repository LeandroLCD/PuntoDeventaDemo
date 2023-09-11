using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuntoDeventa.Core.LocalData.DataBase.Entities
{
    public class ProductEntity
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int BarCode { get; set; }

        public int Code { get; set; }

        public string UDM { get; set; }

        public bool IsOffer { get; set; }

        public float Percentage { get; set; }

        public double PriceGross { get; set; }

        [ForeignKey(typeof(CategoryEntity))]
        public string CategoryId { get; set; }

        [ManyToOne]
        public CategoryEntity Category { get; set; }
    }
}
