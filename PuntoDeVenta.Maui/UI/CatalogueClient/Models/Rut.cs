namespace PuntoDeVenta.Maui.UI.CatalogueClient.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Rut
    {
        public Rut()
        {

        }
        public Rut(string numberDv)
        {
            var split = numberDv.Split('-');
            Number = int.Parse(split[0]);
            Dv = split[1];
        }
        public Rut(int number, string dV)
        {
            Number = number;
            Dv = dV;
        }

        [Required(ErrorMessage = "Número de rut es requerido")]
        [MinLength(6, ErrorMessage = "El rut ingresado no corresponde a un rut valido")]
        [MaxLength(9, ErrorMessage = "El rut ingresado no corresponde a un rut valido")]
        public int Number { get; set; }

        [RegularExpression("^(?:[0-9]|K)$", ErrorMessage = "El digito verificador no es valido.")]
        [Required(ErrorMessage = "Digito verificador es requerido")]
        public string Dv { get; set; }

        public string NumberDv => $"{Number}-{Dv}";
    }
}
