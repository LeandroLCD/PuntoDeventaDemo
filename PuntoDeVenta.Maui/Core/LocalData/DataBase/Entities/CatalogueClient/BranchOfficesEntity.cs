using PuntoDeVenta.Maui.Core.LocalData.DataBase.SQL;

namespace PuntoDeVenta.Maui.Core.LocalData.DataBase.Entities.CatalogueClient
{
    public class BranchOfficesEntity
    {
        [PrimaryKey]
        public int Code { get; set; }

        public string Commune { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public string CommuneName { get; set; }

        [ForeignKey(typeof(ClientEntity))]
        public string ClientId { get; set; }
    }
}
