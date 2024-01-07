using PuntoDeVenta.Maui.Core.LocalData.DataBase;
using PuntoDeVenta.Maui.Core.LocalData.DataBase.Entities.CatalogueClient;
using PuntoDeVenta.Maui.Data.Repository.Auth;
using PuntoDeVenta.Maui.Data.Repository.CatalogueClient;
using PuntoDeVenta.Maui.Domain.UseCase.Auth;
using PuntoDeVenta.Maui.UI.Auth.States;
using System.Diagnostics;

namespace PuntoDeVenta.Maui
{
    public partial class App : Application
    {
        [Obsolete]
        public App(IAuthRepository auth, 
            ICatalogueClientRepository catalogueRepository,
            IDataAccessObject dataAccess,
            ILoginUseCase loginUseCase)
        {
            InitializeComponent();

            ContentPage contentPage = new()
            {
                Content = new StackLayout()
                {
                    BackgroundColor = Colors.White,
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                       new Label()
                       {
                           Text = "SUSCRIBETE a @BlipBlipCode",
                           TextColor = Colors.Red,
                           HorizontalOptions = LayoutOptions.CenterAndExpand,
                           VerticalOptions = LayoutOptions.CenterAndExpand,
                           FontSize = 30,
                           FontAttributes = FontAttributes.Bold,
                       }
                    }
                }
            };
            MainPage = contentPage;
            //TestInsertAllDAO(dataAccess);
            loginTest(auth, loginUseCase);

            //TestCatalogueRepository(catalogueRepository);


        }

        private async void TestCatalogueRepository(ICatalogueClientRepository catalogueRepository)
        {
            var salesRoutes = catalogueRepository.GetCatalogueAsync();
            await foreach (var item in salesRoutes)
            {
                Debug.WriteLine($"BlipBlip: route name {item.Name}" +
                    $" client #:{item.Clients.Count}");

            }
        }

        private async void TestAuthRepository(IAuthRepository auth)
        {
            var isRegister = await registerUserTestAsync(auth);

            

        }

        private async Task<bool> loginTest(IAuthRepository auth, ILoginUseCase loginUseCase)
        {
            //var state = await auth.Login("Blipblipcode@gmail.com", "123abc");
            var state = await loginUseCase.Login(new UI.Auth.Models.AuthDataUser()
            {
                Email = "Blipblipcode@gmail.com",
                Password ="123abc"
            });

            switch (state)
            {
                case AuthStates.Success success:
                    Debug.Assert(success.Data is not null, $"BlipBlipcode: Login is Success");
                    return true;
                case AuthStates.Error error:
                    Debug.WriteLine($"{error.Message}");
                    return false;
                default:
                    throw new NotImplementedException($"auth.Login return the state{state}");
            }
        }

        private async Task<bool> registerUserTestAsync(IAuthRepository auth)
        {
            var registerState = await auth.Register("correo@blipblip.com", "123456");

            switch (registerState)
            {
                case AuthStates.Success success:
                    Debug.Assert(success.Data is not null, $"BlipBlip: Registro Exitoso");
                    return true;
                case AuthStates.Error error:
                    Debug.Fail(error.Message);
                    return false;
                default:
                    throw new NotImplementedException();
            }
        }

        private void TestInsertAllDAO(IDataAccessObject DAO)
        {
            

            var list = new List<ClientEntity>();
            for (int i = 0; i < 10; i++)
            {
                var client = new ClientEntity()
                {
                    Name = $"Cliente de Prueba {i}"
                };
                list.Add(client);
            }
            var route = new SalesRoutesEntity()
            {
                Name = "Insertar Lista",
                Clients = list

            };

            DAO.InsertOrUpdate(route);

            var listTest = DAO.Get<SalesRoutesEntity>(route.Id);
            var assert = listTest.Clients.Count == route.Clients.Count;
            Debug.WriteLine($"BlipBlip Result {assert}");
        }

        private static void TestInserDAO()
        {
            var DAO = new DataAccessObject();
            var route = new SalesRoutesEntity()
            {
                Name = "Prueba Ruta 1"
            };

            DAO.InsertOrUpdate(route);

            var testList = DAO.GetAll<SalesRoutesEntity>();

            var assert = testList.ToList().Exists(it => it.Name == route.Name);
            Debug.WriteLine($"BlipBlip Result {assert}");
        }

        private static void TestDeleteDAO()
        {
            var DAO = new DataAccessObject();
            var route = new SalesRoutesEntity()
            {
                Name = "Prueba Delete1"
            };

            DAO.InsertOrUpdate(route);

            var oldList = DAO.GetAll<SalesRoutesEntity>();

            DAO.Delete(route);

            var newList = DAO.GetAll<SalesRoutesEntity>();

            var assert = !newList.ToList().Exists(it => it.Id == route.Id);

            Debug.WriteLine($"BlipBlip Result {assert}");
        }

        private static void TestUpdateDAO()
        {
            var DAO = new DataAccessObject();
            var route = new SalesRoutesEntity()
            {
                Name = "Prueba Delete1"
            };

            DAO.InsertOrUpdate(route);

            var oldRout = DAO.Get<SalesRoutesEntity>(route.Id);

            var name = oldRout.Name = "Ruta Actualizada";

            DAO.InsertOrUpdate(route);

            var newList = DAO.GetAll<SalesRoutesEntity>();

            var assert = newList.ToList().Exists(it => it.Id == route.Id && it.Name == name);

            Debug.WriteLine($"BlipBlip Result {assert}");
        }
    }
}
