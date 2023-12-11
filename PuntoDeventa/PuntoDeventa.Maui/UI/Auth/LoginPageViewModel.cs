using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.Models;
using PuntoDeventa.Domain.UseCase.Auth;
using PuntoDeventa.Domain.UsesCase.Auth;
using PuntoDeventa.IU;
using PuntoDeventa.IU.Auth.Screen;
using PuntoDeventa.UI.Auth.Models;
using PuntoDeventa.UI.Auth.Screen;
using PuntoDeventa.UI.Auth.States;
using PuntoDeventa.UI.Menu;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

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

        public Command<bool> IsRemembermeCommand { get; set; }

        public Command RecoveryCommand { get; set; }

        #endregion

        #region methods
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
            RecoveryCommand = new Command(async () =>
            {
                await App.Current.MainPage.DisplayAlert("404", "En construción", "ok");
            });

            IsRemembermeCommand = new Command<bool>((isCheked) => { _isRememberme = isCheked; });


        }

        private async void LoginMethods(AuthDataUser dataUser)
        {
            GetAuthStates = AuthStates.Loading.Instance;

            GetAuthStates = await _loginUseCase.Login(DataUser);


        }

        private void HandlerState(AuthStates state)
        {
            switch (state)
            {
                case AuthStates.Loaded loaded:
                    var userCurren = _getUserCurrent.GetUserData();
                    if (userCurren.IsNotNull() && userCurren.IsAuthValid)
                    {
                        App.Current.MainPage = new MenuAppShell();
                        break;
                    }



                    var rememberme = _isRemembermeUseCase.GetRemembermeUser();
                    rememberme?.Apply(() =>
                    {
                        if (rememberme.IsRememberme)
                        {
                            DataUser.Email = rememberme.Email;
                            DataUser.IsRememberme = _isRememberme = rememberme.IsRememberme;
                            NotifyPropertyChanged(nameof(DataUser));
                        }

                    });
                    GridParent?.Apply(() =>
                    {
                        GridParent.Children.Clear();
                        GridParent.Children.Add(new LoginScreen(DataUser, IsRemembermeCommand, LoginCommand, RecoveryCommand));

                    });
                    break;
                case AuthStates.Loading loading:
                    DataUser.ErrorClear();
                    //NotifyPropertyChanged(nameof(DataUser));
                    //Logica para la pantalla de carga
                    GridParent.Children.Clear();
                    GridParent.Children.Add(new LoadingScreen());

                    break;
                case AuthStates.Success success:
                    if (_isRememberme)
                        _isRemembermeUseCase.SetRemembermeUser(new RemembermeUser(DataUser.Email, _isRememberme));
                    // Acciones para success navegamos a al home.
                    GridParent?.Apply(() =>
                    {
                        GridParent.Children.Clear();
                        GridParent.Children.Add(new SuccessScreem());
                        Task.Delay(1000);
                        App.Current.MainPage = new MenuAppShell();
                        return;
                    });

                    break;

                case AuthStates.Error error:
                    // Implementamos logica error
                    NotifyPropertyChanged(nameof(DataUser));
                    GridParent.Children.Clear();
                    GridParent.Children.Add(new ErrorScreen(error.Message, () =>
                    {
                        GetAuthStates = AuthStates.Loaded.Instance;
                    }));
                    break;
            }
        }
        #endregion
    }
}
