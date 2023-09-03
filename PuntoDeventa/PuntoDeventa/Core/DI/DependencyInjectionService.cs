using PuntoDeventa.Core.LocalData;
using PuntoDeventa.Data.Repository.Auth;
using PuntoDeventa.Demo.Domain.UsesCase.Auth.Implementation;
using PuntoDeventa.Domain.UseCase.Auth;
using PuntoDeventa.Domain.UseCase.Auth.Implementation;
using PuntoDeventa.Domain.UsesCase.Auth;
using PuntoDeventa.UI.Auth;
using Xamarin.Forms;

namespace PuntoDeventa.Core.DI
{
    internal class DependencyInjectionService
    {
        private AuthRepository _authRepository;
        private IUserRepository _userRepository;

        public DependencyInjectionService()
        {
            RegisterCoreDependencies();

            RegisterDataDependencies();

            RegisterDomainDependencies();

            RegisterUserInterfaceDependencies();
        }




        /// <summary>
        /// Registra las dependencias de la capa Core utilizando DependencyService.
        /// </summary>
        private void RegisterCoreDependencies()
        {
            //Registro de dependencia DataPreferences
            DependencyService.Register<IDataPreferences, DataPreferences>();
        }
        /// <summary>
        /// Registra las dependencias de la capa Data utilizando DependencyService.
        /// </summary>
        private void RegisterDataDependencies()
        {
            _authRepository = new AuthRepository(DependencyService.Get<IDataPreferences>());
            DependencyService.RegisterSingleton<IAuthRepository>(_authRepository);
            DependencyService.RegisterSingleton<IUserRepository>(_authRepository);
        }
        /// <summary>
        /// Registra las dependencias de la capa Domain utilizando DependencyService.
        /// </summary>
        private void RegisterDomainDependencies()
        {
            _userRepository = DependencyService.Get<IUserRepository>();
            DependencyService.RegisterSingleton<IUserCurrentUseCase>(new UserCurrentUseCase(_userRepository));
            DependencyService.RegisterSingleton<ILoginUseCase>(new LoginUseCase(_authRepository));
            DependencyService.RegisterSingleton<IRegisterUseCase>(new RegisterUseCase(_authRepository));
            DependencyService.RegisterSingleton<IRememberUserUseCase>(new RememberUserUseCase(_userRepository));
        }
        /// <summary>
        /// Registra las dependencias de la capa UserInterface utilizando DependencyService.
        /// </summary>
        private void RegisterUserInterfaceDependencies()
        {
            var login = DependencyService.Get<ILoginUseCase>();
            var isRememberme = DependencyService.Get<IRememberUserUseCase>();
            var userCurrentUseCase = DependencyService.Get<IUserCurrentUseCase>();
            //Inyección por contructor
            DependencyService.RegisterSingleton(new LoginPageViewModel(login, userCurrentUseCase, isRememberme));

            //Inyección por metodo o campo

            //DependencyService.Register<LoginPageViewModel>();
        }

    }
}
