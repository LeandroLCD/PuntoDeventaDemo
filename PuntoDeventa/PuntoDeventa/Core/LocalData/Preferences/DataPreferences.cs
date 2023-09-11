namespace PuntoDeventa.Core.LocalData.Preferences
{
    using Xamarin.Essentials;
    using Newtonsoft.Json;
    using PuntoDeventa.Data.DTO;
    internal class DataPreferences : IDataPreferences
    {
        #region fields
        private UserDataDTO _userData;
        private RemembermeUserDTO _remembermeUser;
        private readonly string _remembermeKey = "RemembermeUser";
        private readonly string _userKey = "UserData";
        #endregion fields
        public RemembermeUserDTO GetRemembermeUser()
        {
            return GetDataPreferencesKey(_remembermeKey, _remembermeUser); ;
        }
        public UserDataDTO GetUserData()
        {
            return GetDataPreferencesKey(_userKey, _userData);
        }

        public void SetRemembermeUser(RemembermeUserDTO user)
        {
            _remembermeUser = SetDataPreferencesKey(_remembermeKey, user);
        }

        public void SetUserData(UserDataDTO user)
        {
            _userData = SetDataPreferencesKey(_userKey, user);
        }

        private T SetDataPreferencesKey<T>(string key, T value)
        {
            Preferences.Set(key, JsonConvert.SerializeObject(value));
            return value;
        }

        private T GetDataPreferencesKey<T>(string key, T model)
        {
            if (Preferences.ContainsKey(key))
            {
                var user = Preferences.Get(key, string.Empty);
                model = JsonConvert.DeserializeObject<T>(user);
            }

            return model;
        }
    }
}
