namespace PuntoDeventa.UI.CatalogueClient.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SalesRoutes
    {
        public SalesRoutes()
        {

        }
        public SalesRoutes(string name)
        {
            Name = name;
        }
        public string Id { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido.")]
        public string Name { get; set; }

        public List<Client> Clients { get; set; }

        //public override bool Equals(object obj)
        //{
        //    if (obj is SalesRoutes other)
        //    {
        //        return Id == other.Id && Name == other.Name && Clients.SequenceEqual(other.Clients);
        //    }
        //    return false;
        //}
        //public override int GetHashCode()
        //{
        //    return Name.GetHashCode() ^ Clients.GetHashCode();
        //}
    }
}
