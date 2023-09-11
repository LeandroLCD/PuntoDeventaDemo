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
    internal class DeleteProductUseCase : BaseCategoryUseCase, IDeleteProductUseCase
    {
        private ICategoryProductRepository _repository;

        public DeleteProductUseCase()
        {
            _repository = DependencyService.Get<ICategoryProductRepository>();
        }
        public async Task<CategoryStates> Delete(Product product)
        {
            return await MakeCallUseCase(product, async () =>
            {
                return await _repository.DeleteProdctAsync(product);
            });
        }
    }
}
