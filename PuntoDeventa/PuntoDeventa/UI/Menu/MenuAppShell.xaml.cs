﻿using PuntoDeventa.Domain.UseCase.CategoryProduct;
using PuntoDeventa.UI.CatalogueClient;
using PuntoDeventa.UI.CategoryProduct;
using Xamarin.Forms;

namespace PuntoDeventa.UI.Menu
{
    public partial class MenuAppShell : Shell
    {
        public MenuAppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            Sync();
        }
        private void Sync()
        {
            var useCase = DependencyService.Get<ISyncDataUseCase>();
            useCase.Sync();
        }

        private void RegisterRoutes()
        {

            #region Categories
            Routing.RegisterRoute(nameof(CategoryHome), typeof(CategoryHome));
            Routing.RegisterRoute(nameof(CategoryDetailPage), typeof(CategoryDetailPage));
            Routing.RegisterRoute(nameof(ProductPage), typeof(ProductPage));
            #endregion

            #region RoutesClient
            Routing.RegisterRoute(nameof(CatalogueClientPage), typeof(CatalogueClientPage));
            Routing.RegisterRoute(nameof(CatalogueDetailPage), typeof(CatalogueDetailPage));

            #endregion
        }
    }
}