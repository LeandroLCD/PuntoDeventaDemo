using PuntoDeventa.Domain;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.Models;
using PuntoDeventa.Domain.UseCase.Auth;
using PuntoDeventa.Domain.UseCase.Auth.Implementation;
using PuntoDeventa.UI.Auth.Models;
using PuntoDeVenta.Domain.Helpers;
using PuntoDeVenta.Domain.Models;
using PuntoDeVenta.Domain.UsesCase.Auth;
using PuntoDeVenta.IU;
using Xamarin.Forms;

namespace PuntoDeventa.UI.Auth
{
    internal class LoginPageViewModel : BaseViewModel
    {
        #region fields
        private readonly ILoginUseCase _loginUseCase;
        private readonly IRememberUserUseCase _isRemembermeUseCase;
        private readonly IUserCurrentUseCase _getUserCurrent;
        private AuthDataUser _dataUser;
        private bool _isRememberme;
        private AuthStates _getStates;
        private bool _isPassword;

        #endregion

        #region constructor


        public LoginPageViewModel()
        {
            _loginUseCase = DependencyService.Get<ILoginUseCase>();
            _isRemembermeUseCase = DependencyService.Get<IRememberUserUseCase>();
            _getUserCurrent = DependencyService.Get<IUserCurrentUseCase>();

            InicializeCommads();
        }

        public LoginPageViewModel(ILoginUseCase loginUseCase, IUserCurrentUseCase userCurrentUseCase, IRememberUserUseCase rememberUserUseCase )
        {
            _loginUseCase = loginUseCase;
            _isRemembermeUseCase = rememberUserUseCase;
            _getUserCurrent = userCurrentUseCase;
            InicializeCommads();
        }
        #endregion

        #region properties

        public AuthDataUser DataUser
        {
            get
            {
                if (_dataUser.IsNull())
                {
                    _dataUser = new AuthDataUser();
                }
                return _dataUser;
            }
            set => SetProperty(ref _dataUser, value);
        }

        public bool IsRememberme
        {
            get => _isRememberme;
            set => SetProperty(ref _isRememberme, value);
        }

        public bool IsPassword
        {
            get => _isPassword;
            set => SetProperty(ref _isPassword, value);
        }
        private AuthStates GetAuthStates
        {
            get => _getStates;
            set
            {
                HandlerState(value);
                SetProperty(ref _getStates, value);
            }
        }

        
        #endregion

        #region commands
        public Command LoginCommand { get; set; }

        public Command IsPasswordCommand { get; set; }
        #endregion

        #region methods
        //test
        public void OnApperning()
        {

        }
        //test
        public UserData GetUserData()
        {
            return _getUserCurrent.GetUserData();
        }
        private void InicializeCommads()
        {
            GetAuthStates = AuthStates.Loaded.Instance;
            LoginCommand = new Command(LoginMethods);
            IsPasswordCommand = new Command(() => { 
                IsPassword = !IsPassword; 
            });

        }

        private async void LoginMethods(object obj)
        {
            GetAuthStates = AuthStates.Loading.Instance;

            GetAuthStates = await _loginUseCase.Login(DataUser);


        }
        private async void HandlerState(AuthStates state)
        {
            switch (state)
            {
                case AuthStates.Loaded loaded:
                    var rememberme = _isRemembermeUseCase.GetRemembermeUser();
                    rememberme?.Apply(() =>
                    {
                        if (rememberme.IsRememberme)
                        {
                            DataUser.Email = rememberme.Email;
                            IsRememberme = true;
                            NotifyPropertyChanged(nameof(DataUser));
                        }

                    });
                    break;
                case AuthStates.Loading loading:
                    DataUser.ErrorClear();
                    NotifyPropertyChanged(nameof(DataUser));
                    IsLoading = true;
                    //Logica para la pantalla de carga
                    break;
                case AuthStates.Success success:
                    if (IsRememberme)
                        _isRemembermeUseCase.SetRemembermeUser(new RemembermeUser(DataUser.Email, true));
                    await App.Current.MainPage.DisplayAlert("Success", $"{((UserData)success.Data).Email}", "OK");

                    // Acciones para success navegamos a al home.
                    break;

                case AuthStates.Error error:
                    // Implementamos logica error
                    if(!DataUser.HasEmail && !DataUser.HasPassword)
                        await App.Current.MainPage.DisplayAlert("Error", error.Message, "OK");
                    else
                    NotifyPropertyChanged(nameof(DataUser));
                    break;
            }
        }
        #endregion
    }
}
