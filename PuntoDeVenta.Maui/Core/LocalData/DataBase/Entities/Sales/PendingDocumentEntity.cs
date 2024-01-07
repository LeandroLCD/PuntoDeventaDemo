using PuntoDeVenta.Maui.Core.LocalData.DataBase.SQL;

namespace PuntoDeVenta.Maui.Core.LocalData.DataBase.Entities.Sales
{
    public class PendingDocumentEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string DocumentType { get; set; }

        public string DocumentDataJson { get; set; }

    }
}
