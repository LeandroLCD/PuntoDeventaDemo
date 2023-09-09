using PuntoDeventa.Core.DI;
using PuntoDeventa.Core.LocalData.Preferences;
using PuntoDeventa.Data.Repository.Auth;
using PuntoDeventa.Data.Repository.CategoryProduct;
using PuntoDeventa.Domain.UseCase.Auth.Implementation;
using PuntoDeventa.Domain.UseCase.CategoryProduct;
using PuntoDeventa.UI.Auth;
using PuntoDeventa.UI.Auth.Models;
using PuntoDeventa.UI.CategoryProduct.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

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

            MainPage = new LoginPage();
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
