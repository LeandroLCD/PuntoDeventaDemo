using PuntoDeventa.Data.Repository.CategoryProduct;
using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.CategoryProduct.States;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

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
