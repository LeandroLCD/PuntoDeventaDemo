
using PuntoDeVenta.Maui.Core.LocalData.DataBase.SQL;

namespace PuntoDeVenta.Maui.Core.LocalData.DataBase.Entities.CatalogueClient
{
    public class SalesRoutesEntity
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string Name { get; set; }

        [OneToMany(CascadeOperation.All)]
        public List<ClientEntity> Clients { get; set; }
    }
}
