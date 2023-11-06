using PuntoDeventa.Data.Repository.CategoryProduct;
using PuntoDeventa.UI.CategoryProduct.States;
using Xamarin.Forms;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct.Implementation
{
    internal class GetProductUseCase : IGetProductUseCase
    {
        private ICategoryProductRepository _repository;

        public GetProductUseCase()
        {
            _repository = DependencyService.Get<ICategoryProductRepository>();
        }
        public CategoryStates Get(string id)
        {
            return _repository.GetProducts(id);
        }
    }
}
