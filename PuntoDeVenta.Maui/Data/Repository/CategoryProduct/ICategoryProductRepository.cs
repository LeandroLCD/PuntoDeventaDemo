using PuntoDeVenta.Maui.UI.CategoryProduct.Models;
using PuntoDeVenta.Maui.UI.CategoryProduct.States;

namespace PuntoDeVenta.Maui.Data.Repository.CategoryProduct
{
    public interface ICategoryProductRepository
    {
        Task<CategoryStates> InsertAsync(Category item);
        Task<CategoryStates> InsertProductAsync(Product item);
        Task<CategoryStates> UpdateAsync(Category item);
        Task<CategoryStates> UpdateProductAsync(Product item);
        Task<CategoryStates> DeleteAsync(Category item);

        Task<CategoryStates> DeleteProductAsync(Product item);
        Task<CategoryStates> GetAsync(string FireBaseId);
        IAsyncEnumerable<Category> GetAllAsync();

        void SyncData();
        List<Category> GetAll();
        List<Product> GetProductsAll();
        CategoryStates GetCategory(string id);
        CategoryStates GetProducts(string id);
    }
}
