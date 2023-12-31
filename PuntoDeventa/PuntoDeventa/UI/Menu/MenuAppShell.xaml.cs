﻿using PuntoDeventa.Domain.UseCase.CategoryProduct;
using PuntoDeventa.UI.CatalogueClient;
using PuntoDeventa.UI.CategoryProduct;
using PuntoDeventa.UI.Reports;
using PuntoDeventa.UI.Sales;
using Xamarin.Forms;

namespace PuntoDeventa.UI.Menu
{
    public partial class MenuAppShell : Shell
    {
        public MenuAppShell()
        {
            RegisterRoutes();
            InitializeComponent();
            var useCase = DependencyService.Get<ISyncDataUseCase>();
            useCase.Sync();

        }

        private static void RegisterRoutes()
        {
            #region Sale
            Routing.RegisterRoute(nameof(SalesPage), typeof(SalesPage));

            Routing.RegisterRoute(nameof(PaymentSalePage), typeof(PaymentSalePage));
            #endregion

            #region Categories
            Routing.RegisterRoute(nameof(CategoryHome), typeof(CategoryHome));
            Routing.RegisterRoute(nameof(CategoryDetailPage), typeof(CategoryDetailPage));
            Routing.RegisterRoute(nameof(ProductPage), typeof(ProductPage));
            #endregion

            #region RoutesClient
            Routing.RegisterRoute(nameof(CatalogueClientPage), typeof(CatalogueClientPage));
            Routing.RegisterRoute(nameof(CatalogueDetailPage), typeof(CatalogueDetailPage));

            #endregion

            #region Reports

            Routing.RegisterRoute(nameof(ReportSalePage), typeof(ReportSalePage));
            Routing.RegisterRoute(nameof(ReportProductPage), typeof(ReportProductPage));

            #endregion

        }
    }
}