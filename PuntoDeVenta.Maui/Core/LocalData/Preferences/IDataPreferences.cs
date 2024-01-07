namespace PuntoDeVenta.Maui.Core.LocalData.Preferences
{
    using PuntoDeVenta.Maui.Data.DTO;
    using PuntoDeVenta.Maui.Data.DTO.Auth;

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
