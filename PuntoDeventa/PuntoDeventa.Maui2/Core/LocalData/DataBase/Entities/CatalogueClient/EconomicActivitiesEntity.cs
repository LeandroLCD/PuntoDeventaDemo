namespace PuntoDeventa.Core.LocalData.DataBase.Entities.CatalogueClient
{
    using SQLite;
    using SQLiteNetExtensions.Attributes;
    public class EconomicActivitiesEntity
    {
        [PrimaryKey, Unique]
        public int Code { get; set; }

        public string Turn { get; set; }

        public string Name { get; set; }

        public bool IsMain { get; set; }

        [ForeignKey(typeof(ClientEntity))]
        public string ClientId { get; set; }

        [ManyToOne]
        public ClientEntity Client { get; set; }
    }
}
