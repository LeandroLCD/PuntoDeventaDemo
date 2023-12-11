using PuntoDeventa.UI.CategoryProduct.States;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct
{
    public interface IGetProductUseCase
    {
        CategoryStates Get(string id);
    }
}
