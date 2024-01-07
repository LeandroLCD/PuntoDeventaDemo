namespace PuntoDeVenta.Maui.Domain.Models
{
    public class ErrorMessage
    {
        public ErrorMessage(string field, string menssage)
        {
            Field = field;
            Message = menssage;
        }
        public string Message { get; set; }
        public string Field { get; set; }

        public override string ToString()
        {
            return $"{Field}: {Message}";
        }
    }
}
