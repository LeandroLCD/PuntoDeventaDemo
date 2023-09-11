namespace PuntoDeventa.Domain.Models
{
    public class RemembermeUser
    {
        public RemembermeUser(string email, bool isRememberme)
        {
            Email = email;
            IsRememberme = isRememberme;
        }
        public string Email { get; set; }

        public bool IsRememberme { get; set; }
    }
}
