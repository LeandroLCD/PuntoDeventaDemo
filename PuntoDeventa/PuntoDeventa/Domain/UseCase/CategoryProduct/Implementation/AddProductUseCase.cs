using PuntoDeventa.Data.Repository.CategoryProduct;
using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.CategoryProduct.States;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct.Implementation
{
    internal class AddProductUseCase : BaseCategoryUseCase, IAddProductUseCase
    {
        private ICategoryProductRepository _repository;

        public AddProductUseCase()
        {
            _repository = DependencyService.Get<ICategoryProductRepository>();
        }
        public async Task<CategoryStates> Insert(Product product)
        {
            return await MakeCallUseCase(product, async () =>
            {
                return await _repository.InsertProductAsync(product);
            });
        }
    }
}
