using PuntoDeventa.UI.CatalogueClient.States;
using PuntoDeventa.UI.CategoryProduct.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuntoDeventa.Domain.UseCase.CatalogueClient
{
    public interface IGetSalesRoutesUseCase
    {
        CatalogeState Get(string id);
    }
}
