using PuntoDeVenta.Maui.Domain.UseCase.Auth;
using PuntoDeVenta.Maui.Core.LocalData.DataBase;
using PuntoDeVenta.Maui.Core.LocalData.Preferences;
using PuntoDeVenta.Maui.Core.Network;
using PuntoDeVenta.Maui.Core.Network.HttpFactory;
using PuntoDeVenta.Maui.Data.Repository.Auth;
using PuntoDeVenta.Maui.Data.Repository.CatalogueClient;
using PuntoDeVenta.Maui.Data.Repository.CategoryProduct;
using PuntoDeVenta.Maui.Data.Repository.EmissionSystem;
using PuntoDeVenta.Maui.Data.Repository.Reports;
using PuntoDeVenta.Maui.Domain.UseCase.Auth.Implementation;

namespace PuntoDeVenta.Maui.Core.DI
{
    internal static class DependencyInjectionService
    {
        public static MauiAppBuilder UseDIService(this MauiAppBuilder builder)
        {
            //builder.Services.AddSingleton;
            //builder.Services.AddTransient;
            builder
                .RegisterCore()
                .RegisterData()
                .RegisterDomain()
                .RegisterIU();
            return builder;
        }

        private static MauiAppBuilder RegisterCore(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<ICustomHttpClientFactory, CustomHttpClientFactory>();
            
            builder.Services.AddTransient<IDataPreferences, DataPreferences>();
            
            builder.Services.AddTransient<IDataAccessObject, DataAccessObject>();

            builder.Services.AddTransient<IDataStore, DataStore>();
            
            builder.Services.AddTransient<IElectronicEmissionSystem, ElectronicEmissionSystem>();

            return builder;
        }
        private static MauiAppBuilder RegisterData(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<IAuthRepository, AuthRepository>();
            builder.Services.AddTransient<IUserRepository, AuthRepository>();
                             
            builder.Services.AddTransient<ICatalogueClientRepository, CatalogueClientRepository>();
                             
            builder.Services.AddTransient<ICategoryProductRepository, CategoryProductRepository>();
                             
            builder.Services.AddTransient<IOpenFacturaRepository, OpenFacturaRepository>();
                             
            builder.Services.AddTransient<IReportRepository, ReportRepository>();






            return builder;
        }
        private static MauiAppBuilder RegisterDomain(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<ILoginUseCase, LoginUseCase>();

            return builder;
        }
        private static MauiAppBuilder RegisterIU(this MauiAppBuilder builder)
        {

            return builder;
        }
    }
}
