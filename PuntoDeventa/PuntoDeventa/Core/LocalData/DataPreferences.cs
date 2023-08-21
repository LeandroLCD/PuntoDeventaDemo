using Newtonsoft.Json;
using PuntoDeventa.Data.DTO;
using PuntoDeventa.Domain.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace PuntoDeventa.Core.LocalData
{
    internal class DataPreferences : IDataPreferences
    {
        #region fields
        private UserDataDTO _userData;


        #endregion fields

        public UserDataDTO GetUserData()
        {

            if (Preferences.ContainsKey("UserData"))
            {
                var user = Preferences.Get("UserData", string.Empty);
                _userData = JsonConvert.DeserializeObject<UserDataDTO>(user);
            }

            return _userData;

        }

        public void SetUserData(UserDataDTO user)
        {
            _userData = user;
            Preferences.Set("UserData", JsonConvert.SerializeObject(user));
        }
    }
}
