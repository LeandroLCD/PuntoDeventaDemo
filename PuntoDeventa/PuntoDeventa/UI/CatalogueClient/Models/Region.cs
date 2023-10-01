namespace PuntoDeventa.UI.CatalogueClient.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Region
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido.")]
        public string Name { get; set; }

        public List<Commune> Communes { get; set; }
    }
}
