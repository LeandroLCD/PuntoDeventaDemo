using PuntoDeventa.Core.DI;
using PuntoDeventa.Core.LocalData;
using PuntoDeventa.Data.Repository.Auth;
using PuntoDeventa.Domain.UseCase.Auth.Implementation;
using PuntoDeventa.UI.Auth;
using PuntoDeventa.UI.Auth.Models;
using Xamarin.Forms;

namespace PuntoDeventa
{
    public partial class App : Application
    {
        public App()
        {
            new DependencyInjectionService();
            InitializeComponent();

            //CallApi();


            //TestUseCase();

            //TestViewmodel();


            MainPage = new LoginPage();
        }

        private void TestViewmodel()
        {
            var viewModel = DependencyService.Get<LoginPageViewModel>();
            var user = viewModel.GetUserData();
        }

        private async void TestUseCase()
        {
            var repo = new AuthRepository(DependencyService.Get<IDataPreferences>());
            var usecase = new LoginUseCase(repo);
            var user = new AuthDataUser() { Email = "blipblipcode@gmail.com", Password = "A1B2C3" };

            var estado = await usecase.Login(user);
        }

        private async void CallApi()
        {
            var repositoy = new AuthRepository(DependencyService.Get<IDataPreferences>());

            //var userCurren = repositoy.GetUserCurren();
            //AuthStates resp = await repositoy.Login("correodeprueba@gmail.com", "A1B2C3");

            //  switch (resp)
            //  {
            //      case AuthStates.Success success:
            //          // Acciones para Success
            //          break;

            //      case AuthStates.Error error:
            //          // Acciones para Error
            //          break;

            //      case AuthStates.Loaded loaded:
            //          // Acciones para Loaded
            //          break;

            //      case AuthStates.Loading loading:
            //          // Acciones para Loading
            //          break;
            //  }
        }
        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
