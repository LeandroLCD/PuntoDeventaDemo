
using PuntoDeVenta.Maui.Core.LocalData.DataBase.SQL;

namespace PuntoDeVenta.Maui.Core.LocalData.DataBase.Entities.CategoryProduct
{
    public class CategoryEntity
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        [OneToMany(CascadeOperation.All)]
        public List<ProductEntity> Products { get; set; }
    }
}
