﻿namespace PuntoDeVenta.Maui.Domain.UseCase.CatalogueClient
{
    using PuntoDeVenta.Maui.UI.CatalogueClient.Model;
    using PuntoDeVenta.Maui.UI.CatalogueClient.States;
    using System.Threading.Tasks;
    public interface IAddSalesRouteUseCase
    {
        Task<CatalogeState> Insert(SalesRoutes route);
    }
}
