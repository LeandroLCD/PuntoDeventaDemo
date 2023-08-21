using PuntoDeventa.Core.LocalData;
using PuntoDeventa.Data.Repository.Auth;
using PuntoDeventa.Domain;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.UI.Auth.Models;
using Xamarin.Forms;
using static PuntoDeventa.Domain.AuthStates;

namespace PuntoDeventa
{
    public partial class App : Application
    {
        public App()
        {
            DependencyService.RegisterSingleton<IDataPreferences>(new DataPreferences());
            InitializeComponent();

            CallApi();





            MainPage = new MainPage();
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
