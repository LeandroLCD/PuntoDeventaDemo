using PuntoDeventa.Core.DI;
using PuntoDeventa.Core.LocalData.DataBase;
using PuntoDeventa.Core.LocalData.DataBase.Entities.CatalogueClient;
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


            //TestCatalogueClient();

            //TestDAO();
        }

        private void TestDAO()
        {
            var DAO = DependencyService.Get<IDataAccessObject>();

            var routes = DAO.GetAll<SalesRoutesEntity>();

            //var client = DAO.GetAll<ClientEntity>();
        }

        private async void TestCatalogueClient()
        {
            var repository = DependencyService.Get<ICatalogueClienteRepository>();

            await foreach (var catalogue in repository.GetCatalogueAsync())
            {
                Console.WriteLine(catalogue.Name);
            }

            var list = repository.GetRoutesAll();

            var client = list.FirstOrDefault().Clients.FirstOrDefault();

            client.Name = "Prueba3";

            // var state = await repository.UpDateClient(client);

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
