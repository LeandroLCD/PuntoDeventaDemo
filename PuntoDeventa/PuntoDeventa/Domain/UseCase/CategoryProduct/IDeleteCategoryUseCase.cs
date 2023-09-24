﻿using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.CategoryProduct.States;
using System.Threading.Tasks;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct
{
    public interface IDeleteCategoryUseCase
    {
        Task<CategoryStates> Delete(Category category);
    }
}
