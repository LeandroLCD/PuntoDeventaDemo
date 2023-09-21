using PuntoDeventa.UI.CategoryProduct.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct
{
    public interface IGetProductUseCase
    {
        CategoryStates Get(string id);
    }
}
