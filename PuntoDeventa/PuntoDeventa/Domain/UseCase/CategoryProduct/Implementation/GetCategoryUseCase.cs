using PuntoDeventa.Data.Repository.CategoryProduct;
using PuntoDeventa.UI.CategoryProduct.States;
using Xamarin.Forms;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct.Implementation
{
    internal class GetCategoryUseCase : IGetCategoryUseCase
    {
        private ICategoryProductRepository _repository;

        public GetCategoryUseCase()
        {
            _repository = DependencyService.Get<ICategoryProductRepository>();
        }
        public CategoryStates Get(string id)
        {
           return _repository.GetCategory(id);
        }
    }
}
