using PuntoDeventa.Data.Repository.CategoryProduct;
using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.CategoryProduct.States;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct.Implementation
{
    internal class DeleteCategoryUseCase : BaseCategoryUseCase, IDeleteCategoryUseCase
    {
        private ICategoryProductRepository _repository;

        public DeleteCategoryUseCase()
        {
            _repository = DependencyService.Get<ICategoryProductRepository>();
        }
        public async Task<CategoryStates> Delete(Category category)
        {
            return await MakeCallUseCase(category, async () =>
            {
                return await _repository.DeleteAsync(category);
            });
        }
    }
}
