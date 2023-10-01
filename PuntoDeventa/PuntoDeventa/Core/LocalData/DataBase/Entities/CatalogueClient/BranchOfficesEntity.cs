using SQLite;
using SQLiteNetExtensions.Attributes;

namespace PuntoDeventa.Core.LocalData.DataBase.Entities.CatalogueClient
{
    public class BranchOfficesEntity
    {
        [PrimaryKey, Unique]
        public int Code { get; set; }

        public string Commune { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public string CommuneName { get; set; }

        [ForeignKey(typeof(ClientEntity))]
        public string ClientId { get; set; }

        [ManyToOne]
        public ClientEntity Client { get; set; }
    }
}
