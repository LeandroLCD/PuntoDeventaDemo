using PuntoDeventa.Data.Repository.CategoryProduct;
using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.CategoryProduct.States;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct.Implementation
{
    internal class EditProductUseCase : BaseCategoryUseCase, IEditProductUseCase
    {
        private ICategoryProductRepository _repository;

        public EditProductUseCase()
        {
            _repository = DependencyService.Get<ICategoryProductRepository>();
        }
        public async Task<CategoryStates> Edit(Product product)
        {
            return await MakeCallUseCase(product, async () =>
            {
                return await _repository.UpdateProductAsync(product);
            });
        }
    }
}
