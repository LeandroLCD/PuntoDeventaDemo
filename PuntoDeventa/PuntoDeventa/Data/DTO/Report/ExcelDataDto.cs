using System.Collections.Generic;

namespace PuntoDeventa.Data.DTO.Report
{
    public class ExcelDataDto
    {
        public List<string> Headers { get; set; }
        public List<List<string>> Values { get; set; }
    }
}
