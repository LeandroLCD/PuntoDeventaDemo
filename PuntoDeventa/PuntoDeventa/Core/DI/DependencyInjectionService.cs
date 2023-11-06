using PuntoDeventa.Core.LocalData.DataBase;
using PuntoDeventa.Core.LocalData.Files;
using PuntoDeventa.Core.LocalData.Preferences;
using PuntoDeventa.Core.Network;
using PuntoDeventa.Data.Repository.Auth;
using PuntoDeventa.Data.Repository.CatalogueClient;
using PuntoDeventa.Data.Repository.CategoryProduct;
using PuntoDeventa.Data.Repository.EmissionSystem;
using PuntoDeventa.Demo.Domain.UsesCase.Auth.Implementation;
using PuntoDeventa.Domain.UseCase.Auth;
using PuntoDeventa.Domain.UseCase.Auth.Implementation;
using PuntoDeventa.Domain.UseCase.CatalogueClient;
using PuntoDeventa.Domain.UseCase.CatalogueClient.Implementation;
using PuntoDeventa.Domain.UseCase.CategoryProduct;
using PuntoDeventa.Domain.UseCase.CategoryProduct.Implementation;
using PuntoDeventa.Domain.UseCase.Sales;
using PuntoDeventa.Domain.UseCase.Sales.Implementation;
using PuntoDeventa.Domain.UsesCase.Auth;
using PuntoDeventa.UI.Auth;
using Syncfusion.Licensing;
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

            SyncfusionLicenseProvider.RegisterLicense(LicenseProvider.LicenceKey);


        }




        /// <summary>
        /// Registra las dependencias de la capa Core utilizando DependencyService.
        /// </summary>
        private void RegisterCoreDependencies()
        {
            //Registro de dependencia DataPreferences
            DependencyService.Register<IFileManager, FileManager>();
            DependencyService.Register<IDataPreferences, DataPreferences>();
            DependencyService.Register<IDataAccessObject, DataAccessObject>();
            DependencyService.Register<IDataStore, DataStore>();
            DependencyService.Register<IElectronicEmissionSystem, ElectronicEmissionSystem>();



        }
        /// <summary>
        /// Registra las dependencias de la capa Data utilizando DependencyService.
        /// </summary>
        private void RegisterDataDependencies()
        {

            _authRepository = new AuthRepository(DependencyService.Get<IDataPreferences>());
            DependencyService.RegisterSingleton<IAuthRepository>(_authRepository);
            DependencyService.RegisterSingleton<IUserRepository>(_authRepository);
            DependencyService.Register<ICategoryProductRepository, CategoryProductRepository>();
            DependencyService.Register<ICatalogueClientRepository, CatalogueClientRepository>();
            DependencyService.Register<IOpenFacturaRepository, OpenFacturaRepository>();
        }
        /// <summary>
        /// Registra las dependencias de la capa Domain utilizando DependencyService.
        /// </summary>
        private void RegisterDomainDependencies()
        {
            #region Auth
            _userRepository = DependencyService.Get<IUserRepository>();
            DependencyService.RegisterSingleton<IUserCurrentUseCase>(new UserCurrentUseCase(_userRepository));
            DependencyService.RegisterSingleton<ILoginUseCase>(new LoginUseCase(_authRepository));
            DependencyService.RegisterSingleton<IRegisterUseCase>(new RegisterUseCase(_authRepository));
            DependencyService.RegisterSingleton<IRememberUserUseCase>(new RememberUserUseCase(_userRepository));
            #endregion

            #region Categories
            DependencyService.Register<IGetCategoryListUseCase, GetCategoryListUseCase>();
            DependencyService.Register<IGetCategoryUseCase, GetCategoryUseCase>();
            DependencyService.Register<IGetProductUseCase, GetProductUseCase>();

            DependencyService.Register<ISyncDataUseCase, SyncDataUseCase>();

            DependencyService.Register<IAddCategoryUseCase, AddCategoryUseCase>();
            DependencyService.Register<IAddProductUseCase, AddProductUseCase>();

            DependencyService.Register<IDeleteCategoryUseCase, DeleteCategoryUseCase>();
            DependencyService.Register<IDeleteProductUseCase, DeleteProductUseCase>();

            DependencyService.Register<IEdictCategoryUseCase, EdictCategoryUseCase>();
            DependencyService.Register<IEditProductUseCase, EditProductUseCase>();

            #endregion

            #region Client


            DependencyService.Register<IAddClientUseCase, AddClientUseCase>();
            DependencyService.Register<IAddSalesRouteUseCase, AddSalesRouteUseCase>();

            DependencyService.Register<IDeleteClientUseCase, DeleteClientUseCase>();
            DependencyService.Register<IDeleteRouteUseCase, DeleteRouteUseCase>();

            DependencyService.Register<IGetSalesRoutesUseCase, GetSalesRoutesUseCase>();
            DependencyService.Register<IGetRoutesUseCase, GetRroutesUseCase>();
            DependencyService.Register<ISyncCatalogueUseCase, SyncCatalogueUseCase>();

            DependencyService.Register<ITributaryInformationUseCase, TributaryInformationUseCase>();
            DependencyService.Register<IUpdateClient, UpdateClient>();

            #endregion

            #region Sales
            DependencyService.Register<IEmitFacturaUseCase, EmitFacturaUseCase>();
            DependencyService.Register<IEmitNotaDePedidoUseCase, InsertNotaDePedidoUseCase>();
            DependencyService.Register<ISyncInformationTributaryUseCase, SyncInformationTributaryUseCase>();

            #endregion

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
