using PuntoDeventa.Data.DTO;

namespace PuntoDeventa.Core.LocalData.Preferences
{
    using Newtonsoft.Json;
    using PuntoDeventa.Data.DTO.Auth;
    using System;

    internal class InClassName<TT>
    {
        public InClassName(string key, TT value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; private set; }
        public TT Value { get; private set; }
    }

    internal class DataPreferences : IDataPreferences
    {
        #region fields
        private UserDataDTO _userData;
        private RemembermeUserDTO _rememberMe;
        private EcommerceDTO _ecommerce;
        private readonly string _rememberMeKey = "RememberMeUser";
        private readonly string _userKey = "UserData";
        private readonly string _ecommerceKey = "EcommerceData";
        #endregion fields
        public RemembermeUserDTO GetRememberMeUser()
        {
            return GetDataPreferencesKey(_rememberMeKey, _rememberMe); ;
        }
        public UserDataDTO GetUserData()
        {
            return GetDataPreferencesKey(_userKey, _userData);
        }
        public EcommerceDTO GetEcommerceData()
        {
            return GetDataPreferencesKey(_ecommerceKey, _ecommerce);
        }
        public void SetEcommerceData(EcommerceDTO ecommerce)
        {
            _ecommerce = SetDataPreferencesKey(new InClassName<EcommerceDTO>(_ecommerceKey, ecommerce));
        }
        public void SetRememberMeUser(RemembermeUserDTO user)
        {
            _rememberMe = SetDataPreferencesKey(new InClassName<RemembermeUserDTO>(_rememberMeKey, user));
        }

        public void SetUserData(UserDataDTO user)
        {
            user.DateLogin = DateTime.Now;
            _userData = SetDataPreferencesKey(new InClassName<UserDataDTO>(_userKey, user));
        }

        private T SetDataPreferencesKey<T>(InClassName<T> inClassName)
        {
            Preferences.Set(inClassName.Key, JsonConvert.SerializeObject(inClassName.Value));
            return inClassName.Value;
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
