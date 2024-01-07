﻿namespace PuntoDeVenta.Maui.Domain.UseCase.CatalogueClient
{
    using PuntoDeVenta.Maui.UI.CatalogueClient.Model;
    using PuntoDeVenta.Maui.UI.CatalogueClient.States;
    using System.Threading.Tasks;
    public interface IAddClientUseCase
    {
        Task<CatalogeState> Insert(Client client);
    }
}
