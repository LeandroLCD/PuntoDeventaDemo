using SQLite;

namespace PuntoDeventa.Core.LocalData.DataBase.Entities.Sales
{
    public class PendingDocumentEntity
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        public string DocumentType { get; set; }

        public string DocumentDataJson { get; set; }

    }
}
