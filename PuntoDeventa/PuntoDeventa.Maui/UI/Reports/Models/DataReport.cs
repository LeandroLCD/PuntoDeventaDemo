using PuntoDeventa.Data.Models;
using System;
using System.IO;

namespace PuntoDeventa.UI.Reports.Models
{
    public abstract class DataReport
    {
        public string Id { get; set; }
        public string Rut { get; set; }
        public DteType Dte { get; set; }
        public string Name { get; set; }
        public DateTime Delivery { get; set; }
        public long Invoice { get; set; }
        public string SellerCode { get; set; }
        public Stream Photo { get; set; }
    }
}
