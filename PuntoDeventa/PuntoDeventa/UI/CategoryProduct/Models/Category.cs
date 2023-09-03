using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PuntoDeventa.UI.CategoryProduct.Models
{
    public class Category
    {

        public string Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoria es requerido.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La marca de la categoria es requerido.")]
        public string Brand { get; set; }

        public List<Product> Products { get; set; }
    }


}
