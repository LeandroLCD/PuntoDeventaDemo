using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.CategoryProduct.States;
using System.Threading.Tasks;

namespace PuntoDeventa.Data.Repository.CategoryProduct
{
    public interface ICategoryProductRepository
    {
        Task<CategoryStates> InsertAsync(Category item);
        Task<CategoryStates> InsertProductAsync(Product item);
        Task<CategoryStates> UpdateAsync(Category item);
        Task<CategoryStates> UpdateProductAsync(Product item);
        Task<CategoryStates> DeleteAsync(Category item);

        Task<CategoryStates> DeleteProdctAsync(Product item);
        Task<CategoryStates> GetAsync(string FireBaseId);
        // IEnumerable<Category> GetAllAsync();
    }
}
