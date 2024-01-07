using Newtonsoft.Json;
using PuntoDeventa.Data.Models;
using System;

namespace PuntoDeventa.Data.DTO.EmissionSystem.Dtes.Header
{
    public class IdDoc
    {
        public IdDoc(DteType type, DateTime date, int frmPago, int tpoTranCompra = 1, int tpoTranVenta = 1)
        {
            DteNumber = (int)type;
            FchEmis = $"{date.Year}-{date.Month:D2}-{date.Day:D2}";
            TpoTranCompra = tpoTranCompra;
            TpoTranVenta = tpoTranVenta;
            FmaPago = frmPago;
        }
        [JsonProperty("TipoDTE")]
        public int DteNumber { get; set; }

        [JsonProperty("Folio")]
        public int Folio { get; set; }

        [JsonProperty("FchEmis")]
        public string FchEmis { get; set; }

        [JsonProperty("TpoTranCompra")]
        public int TpoTranCompra { get; set; }

        [JsonProperty("TpoTranVenta")]
        public int TpoTranVenta { get; set; }

        [JsonProperty("FmaPago")]
        public int FmaPago { get; set; }
    }
}