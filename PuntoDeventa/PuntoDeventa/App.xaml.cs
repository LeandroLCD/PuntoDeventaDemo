using PuntoDeventa.Core.DI;
using PuntoDeventa.Data.Repository.CatalogueClient;
using PuntoDeventa.UI.Auth;
using System;
using System.Linq;
using Xamarin.Forms;

namespace PuntoDeventa
{
    public partial class App : Application
    {
        public App()
        {
            new DependencyInjectionService();

            InitializeComponent();

            MainPage = new LoginPage();

            TestCatalogueClient();
        }

        private async void TestCatalogueClient()
        {
            var repository = DependencyService.Get<ICatalogueClienteRepository>();  

            await foreach( var catalogue in repository.GetCatalogueAsync()) 
            {
                    Console.WriteLine(catalogue.Name);
            }

            var list = repository.GetRoutesAll();

            var client = list.FirstOrDefault().Clients.FirstOrDefault();

            client.Name = "Prueba2";

            var state = await repository.DeleteClient(client);

            var client2 = list.FirstOrDefault().Clients.FirstOrDefault();


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
