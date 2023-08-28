using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.Models;
using PuntoDeventa.Domain.UseCase.Auth;
using PuntoDeventa.UI.Auth.Models;
using PuntoDeventa.UI.Auth.Screen;
using PuntoDeventa.UI.Auth.States;
using PuntoDeVenta.Domain.Helpers;
using PuntoDeVenta.Domain.Models;
using PuntoDeVenta.Domain.UsesCase.Auth;
using PuntoDeVenta.IU;
using PuntoDeVenta.IU.Auth.Screen;
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

        public LoginPageViewModel(ILoginUseCase loginUseCase, IUserCurrentUseCase userCurrentUseCase, IRememberUserUseCase rememberUserUseCase)
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
        private Grid GridParent { get; set; }

        #endregion

        #region commands
        public Command<AuthDataUser> LoginCommand { get; set; }

        public Command IsPasswordCommand { get; set; }

        public Command RecoveryCommand { get; set; }
        
        #endregion

        #region methods
        //test
        public void OnApperning(Grid gridParent)
        {
            GridParent = gridParent;
            GetAuthStates = AuthStates.Loaded.Instance;
        }
        //test
        public UserData GetUserData()
        {
            return _getUserCurrent.GetUserData();
        }
        private void InicializeCommads()
        {
            
            LoginCommand = new Command<AuthDataUser>(LoginMethods);
            IsPasswordCommand = new Command(() =>
            {
                IsPassword = !IsPassword;
            });

        }

        private async void LoginMethods(AuthDataUser dataUser)
        {
            GetAuthStates = AuthStates.Loading.Instance;
            DataUser.ErrorClear();
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
                    GridParent?.Apply(() =>
                    {
                        GridParent.Children.Clear();
                        GridParent.Children.Add(new LoginScreen(DataUser, IsRememberme, LoginCommand, RecoveryCommand));

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
                    if (DataUser.HasEmail.Equals(false) && DataUser.HasPassword.Equals(false))
                    {
                       
                            GridParent.Children.Clear();
                            GridParent.Children.Add(new ErrorScreen(error.Message, () =>
                            {

                                GetAuthStates = AuthStates.Loaded.Instance;

                            }, height: GridParent.Height, width: GridParent.Width));
                        
                    }
                        
                            //await App.Current.MainPage.DisplayAlert("Error", error.Message, "OK");
                    else
                        NotifyPropertyChanged(nameof(DataUser));
                    break;
            }
        }
        #endregion
    }
}
