using PuntoDeventa.Data.DTO;
using PuntoDeventa.Data.DTO.Auth;

namespace PuntoDeventa.Core.LocalData.Preferences
{
    public interface IDataPreferences
    {
        UserDataDTO GetUserData();

        RemembermeUserDTO GetRememberMeUser();

        EcommerceDTO GetEcommerceData();
        void SetEcommerceData(EcommerceDTO ecommerce);

        void SetUserData(UserDataDTO user);

        void SetRememberMeUser(RemembermeUserDTO user);
    }
}
