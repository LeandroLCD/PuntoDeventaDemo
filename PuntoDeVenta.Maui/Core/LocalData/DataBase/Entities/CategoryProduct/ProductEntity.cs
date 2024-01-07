
using PuntoDeVenta.Maui.Core.LocalData.DataBase.SQL;

namespace PuntoDeVenta.Maui.Core.LocalData.DataBase.Entities.CategoryProduct
{
    public class ProductEntity
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public long BarCode { get; set; }

        public int Code { get; set; }

        public string UDM { get; set; }

        public bool IsOffer { get; set; }

        public bool InReport { get; set; }

        public float Percentage { get; set; }

        public double PriceGross { get; set; }

        [ForeignKey(typeof(CategoryEntity))]
        public string CategoryId { get; set; }
    }
}
