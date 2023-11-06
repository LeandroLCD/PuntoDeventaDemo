using Newtonsoft.Json;
using System.Collections.Generic;

namespace PuntoDeventa.Data.DTO.EmissionSystem.Dtes.Detail
{
    public class DetailDTO
    {

        [JsonProperty("NroLinDet")]
        public int Id { get; set; }

        [JsonProperty("CdgItem")]
        public List<ItemCode> CdgItem { get; set; }

        [JsonProperty("NmbItem")]
        public string NmbItem { get; set; }

        [JsonProperty("QtyItem")]
        public int QtyItem { get; set; }

        [JsonProperty("PrcItem")]
        public int PriceItem { get; set; }

        [JsonProperty("MontoItem")]
        public int Amount => QtyItem == 0 ? 0 : PriceItem * QtyItem;
    }
}