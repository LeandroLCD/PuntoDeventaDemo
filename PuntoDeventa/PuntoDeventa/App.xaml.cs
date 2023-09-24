using PuntoDeventa.Core.DI;
using PuntoDeventa.Core.LocalData.Preferences;
using PuntoDeventa.Data.Repository.Auth;
using PuntoDeventa.Data.Repository.CatalogueClient;
using PuntoDeventa.Data.Repository.CategoryProduct;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.UseCase.Auth.Implementation;
using PuntoDeventa.Domain.UseCase.CategoryProduct;
using PuntoDeventa.UI.Auth;
using PuntoDeventa.UI.Auth.Models;
using PuntoDeventa.UI.CatalogueClient.Model;
using PuntoDeventa.UI.CatalogueClient.Models;
using PuntoDeventa.UI.CatalogueClient.States;
using PuntoDeventa.UI.CategoryProduct.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PuntoDeventa
{
    public partial class App : Application
    {
        CancellationTokenSource TokenSource { get; set; }
        public App()
        {
            new DependencyInjectionService();
            
            InitializeComponent();

            //CallApi();

            TokenSource = new CancellationTokenSource();
            //TestUseCase();

            //TestViewmodel();

            //TestBrand();

            //TestGetCategoryUseCase();

            //TestInserRoute();

            //TestInfoClient();

            //TestInserClient();

            TestCallTwoClientAsync();

            MainPage = new LoginPage();
        }

        private async void TestCallTwoClientAsync()
        {
            var repository = new CatalogueClienteRepository();
            await foreach (var r in repository.GetCatalogueAsync())
            {

                Console.WriteLine($"{r.Id}_{r.Name}. nro Client={r.Clients.Count}");
            }
        }

        private void TestInserClient()
        {
            var repo = new CatalogueClienteRepository();
            string[] listaRut = new string[]
                   {
                       "10476898-9", "18891918-9", "13351250-0", "77296181-2", "12005952-1",
                       "76955852-7", "76074130-2", "9592017-9", "7609454-3", "10898846-0",
                       "14538326-9", "6113540-5", "4620741-6", "76084090-4", "8170945-9",
                       "10764166-1", "7324643-1", "10551305-4", "77216158-1", "14015966-2",
                       "15871303-9", "14585787-2", "17059748-6", "76915034-K", "12149910-K",
                       "6605908-1", "9338872-0", "14529234-4", "77307157-8", "6816221-1"
                   };



                string[] listRut2 = new string[]
                   {
                       "76752124-3", "8832005-0", "17895850-K", "18402624-4", "18402624-4",
                       "77499341-K", "7002090-4", "12783114-9", "20509017-7", "10728398-6",
                       "77614032-5", "20025470-8", "8896095-5", "8896095-5", "10845283-8",
                       "14407959-0", "7376581-1", "7611538-9", "9247418-6", "11998459-9",
                       "17627245-7", "76726064-4", "77494406-0", "11069557-8", "8616673-9",
                       "16844229-7", "12180085-3", "76476721-7", "11761638-k", "4276742-5"
                   };

                 string[] listRut3 = new string[]
                   {
                       "7754742-8", "17155563-9", "9019223-K", "11233779-2", "6287155-5",
                       "76935299-6", "13203662-4", "15133242-0", "8417349-5", "76999262-6",
                       "15133242-0", "15133116-5", "12470955-5", "77427747-1", "77705382-5",
                       "8899067-6", "76359693-1", "76313002-9", "11280250-9", "77409529-2",
                       "17137383-2", "10475099-0", "12368603-9", "11143995-8", "11165027-6",
                       "10791918-K", "76919489-4", "8628420-0", "6444056-K", "9132857-7"
                   };

                 string[] listRut4 = new string[]
                   {
                       "7395016-3", "7124771-6", "6909148-2", "77400550-1", "12781376-0",
                       "77469546-K", "9916235-K", "9695818-8", "15497085-1", "12886527-6",
                       "11995594-7", "13202410-3", "4581678-8", "76547812-K", "12671790-3",
                       "13202449-9", "5655875-6", "76640206-2", "17716044-K", "9709434-9",
                       "76285990-4", "9867646-5", "8616673-9", "5101022-1", "10771612-2",
                       "11761082-9", "77671365-1", "10786251-K", "77656817-1", "12781430-9"
                   };

            List<string> errorList = new List<string>();

            listRut4.ForEach(async rut => { 
            
                var _rut = new Rut(rut);
                await Task.Delay(500);
                var resp = await repo.GetTributaryInformation(_rut);
                switch(resp)
                {
                    case CatalogeState.Success success:
                        Client client = (Client)success.Data;
                        client.RouteId = "-Nf3e8PPwLTyUpy58xob";
                        if (client.EconomicActivities.IsNotNull() && client.EconomicActivities.Count == 0)
                        {
                            break;
                        }
                            var insert = await repo.InsertClient(client);
                        break;

                     case CatalogeState.Error error:
                        errorList.Add(error.Message);
                        break;

                }

                
            
            });
            
        }

        private async void TestInfoClient()
        {
            var repo = new CatalogueClienteRepository();
           
            var rut = new Rut(18402624, "4");

            var resp = await repo.GetTributaryInformation(rut);
        }

        private async void TestInserRoute()
        {
            //var Routes = new SalesRoutes("Suscribete!!!"); //-Nf3bHCp21hAHImxhf44
            var Route = new SalesRoutes("Managua");
            var repo = new CatalogueClienteRepository();

            var Route2 = new SalesRoutes("Maule");

            var resp = await repo.InsertRoute(Route); //-Nf3e6aVb3-qw7eRywzO
            var resp2 = await repo.InsertRoute(Route2); //-Nf3e8PPwLTyUpy58xob
        }

        private async void TestGetCategoryUseCase()
        {
            var useCase = DependencyService.Get<IGetCategoryListUseCase>();
            List<Category> list = new List<Category>();
            
            int x = 0;
            await foreach(var category in useCase.Emit(TokenSource.Token))
            {
                list.AddRange(category);
                if(x > 0)
                {
                    TokenSource.Cancel();
                }
                x++;
            }
        }

        private async void TestBrand()
        {
            var repository = new CategoryProductRepository();


            var Category = new Category()
            {
                Id= "-NdNlhlKWHhUgZLHgaWN",
                Name = "Promacion",
                Brand = "Blip Blip Code!!!!"
            };

            //var resp = await repository.InsertAsync(Category);

            //var product = new List<Product>() {



            //};

            //for(var i = 0; i < 10; i++)
            //{
            //    product.Add(new Product()
            //    {
            //        CategoryId = Category.Id,
            //        Name = $"ProductTest{i}",
            //        PriceGross = i * 5896,
            //        IVA = 0.19f
            //    });
            //}
            //product.ForEach(async p =>
            //{
            //    var rep = await repository.InsertProductAsync(p);
            //});



            //    var prod = new Product()
            //    {
            //        CategoryId = Category.Id,
            //        Name = $"ProductTest",
            //        Id = "-NdNnuhUwY8dFvm-dQZV"
            //    };

            //    var respn = await repository.DeleteProdctAsync(prod);

            


            await foreach (var item in repository.GetAllAsync())
            {
                Console.WriteLine(item.Name);
                await Task.Delay(150);
            }

            //var rsp = await repository.GetAsync("-NdNlhlKWHhUgZLHgaWN");
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
