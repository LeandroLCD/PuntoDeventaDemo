using PuntoDeventa.Data.Repository.CategoryProduct;
using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.CategoryProduct.States;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct.Implementation
{
    internal class EdictCategoryUseCase : BaseCategoryUseCase, IEdictCategoryUseCase
    {
        private ICategoryProductRepository _repository;

        public EdictCategoryUseCase()
        {
            _repository = DependencyService.Get<ICategoryProductRepository>();
        }
        public async Task<CategoryStates> Edit(Category category)
        {
            return await MakeCallUseCase(category, async () =>
            {
                return await _repository.UpdateAsync(category);
            });
        }
    }
}
