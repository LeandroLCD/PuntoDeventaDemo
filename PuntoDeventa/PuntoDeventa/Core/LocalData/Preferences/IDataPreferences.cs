using PuntoDeventa.Data.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Text;

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
