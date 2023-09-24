using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.CategoryProduct.States;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct
{
    public interface IEdictCategoryUseCase
    {
        Task<CategoryStates> Edit(Category category);
    }
}
