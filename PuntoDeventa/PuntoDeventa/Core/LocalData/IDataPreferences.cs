using PuntoDeventa.Data.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuntoDeventa.Core.LocalData
{
    public interface IDataPreferences
    {
        UserDataDTO GetUserData();

        void SetUserData(UserDataDTO user);
    }
}
