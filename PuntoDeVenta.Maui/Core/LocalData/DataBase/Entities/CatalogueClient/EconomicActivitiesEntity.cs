using PuntoDeVenta.Maui.Core.LocalData.DataBase.SQL;

namespace PuntoDeVenta.Maui.Core.LocalData.DataBase.Entities.CatalogueClient
{
    public class EconomicActivitiesEntity
    {
        [PrimaryKey]
        public int Code { get; set; }

        public string Turn { get; set; }

        public string Name { get; set; }

        public bool IsMain { get; set; }

        [ForeignKey(typeof(ClientEntity))]
        public string ClientId { get; set; }

    }
}
