using PuntoDeventa.UI.CategoryProduct.States;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct
{
    public interface IGetCategoryUseCase
    {
        CategoryStates Get(string id);
    }
}
