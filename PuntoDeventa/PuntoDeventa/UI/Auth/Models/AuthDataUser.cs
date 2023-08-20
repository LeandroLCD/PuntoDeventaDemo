using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PuntoDeventa.UI.Auth.Models
{
    public class AuthDataUser
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "El campo {0} no corresponde a un {0}")]
        public string Email { get; set; }


        public string EmailErrorText { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DisplayName("Password")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Za-z]).+$", ErrorMessage = "La contraseña debe contener al menos una letra y u número en 6 carácteres.")]
        public string Password { get; set; }

        public string PasswordErrorText { get; set; }
    }
}
