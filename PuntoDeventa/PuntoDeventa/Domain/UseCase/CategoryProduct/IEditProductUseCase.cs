using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.CategoryProduct.States;
using System.Threading.Tasks;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct
{
    public interface IEditProductUseCase
    {
        Task<CategoryStates> Edit(Product product);
    }
}
