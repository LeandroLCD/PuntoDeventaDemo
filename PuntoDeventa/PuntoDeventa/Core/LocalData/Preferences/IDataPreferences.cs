using PuntoDeventa.Data.DTO.Auth;

namespace PuntoDeventa.Core.LocalData.Preferences
{
    public interface IDataPreferences
    {
        UserDataDTO GetUserData();

        RemembermeUserDTO GetRemembermeUser();

        void SetUserData(UserDataDTO user);

        void SetRemembermeUser(RemembermeUserDTO user);
    }
}
