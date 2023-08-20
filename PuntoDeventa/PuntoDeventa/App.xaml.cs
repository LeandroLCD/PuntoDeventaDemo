using PuntoDeventa.Data.Repository.Auth;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.UI.Auth.Models;
using Xamarin.Forms;

namespace PuntoDeventa
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            CallApi();





            MainPage = new MainPage();
        }
        private async void CallApi()
        {
            var repositoy = new AuthRepository();
          var resp = await repositoy.Login("blipblipcode@gmail.com", "A1B2C3");
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
