using PuntoDeventa.Core.LocalData;
using PuntoDeventa.Core.Network;
using PuntoDeventa.Data.DTO;
using PuntoDeventa.Data.Mappers;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.Models;
using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.CategoryProduct.States;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PuntoDeventa.Data.Repository.CategoryProduct
{
    internal class CategoryProductRepository: BaseRepository, ICategoryProductRepository
    {
        private IDataStore _dataStore;
        private IDataPreferences _dataPreferences;
        private string tokenID;

        public CategoryProductRepository()
        {
            _dataStore = DependencyService.Get<IDataStore>();
            _dataPreferences = DependencyService.Get<IDataPreferences>();
            tokenID = _dataPreferences.GetUserData().IdToken;
        }

        public async Task<CategoryStates> DeleteAsync(Category item)
        {
            var resultType = await MakeCallNetwork<Category>(() => {
                return _dataStore.DeleteAsync<Category>(GetUri($"CategoryProduct/{item.Id}"));
            });

            return ResultTypeToCategoryStates(item, resultType);

        }

        public async Task<CategoryStates> DeleteProdctAsync(Product item)
        {
            var resultType = await MakeCallNetwork<Product>(() => {
                return _dataStore.DeleteAsync<Product>(GetUri($"CategoryProduct/{item.CategoryId}/Products/{item.Id}"));
            });

            return ResultTypeToCategoryStates(item, resultType);
        }

        public async Task<CategoryStates> GetAsync(string FireBaseId)
        {
            var resultType = await MakeCallNetwork<CategoryDTO>(() => {
                return _dataStore.GetAsync<CategoryDTO>(GetUri($"CategoryProduct/{FireBaseId}"));
            });

            if (resultType.Success)
            {
                return new CategoryStates.Success(resultType.Data.ToCategory(FireBaseId));
            }
            else
            {
                return new CategoryStates.Error(string.Join(Environment.NewLine, resultType.Errors));
            }
        }

        public async Task<CategoryStates> InsertAsync(Category item)
        {
            var resultType = await MakeCallNetwork<CategoryDTO>(() => {
                return _dataStore.PostAsync(item, GetUri($"CategoryProduct"));
            });

            return ResultTypeToCategoryStates(item, resultType);
        }

       

        public async Task<CategoryStates> InsertProductAsync(Product item)
        {
            var resultType = await MakeCallNetwork<ProductDTO>(() => {
                return _dataStore.PostAsync(item, GetUri($"CategoryProduct/{item.CategoryId}/Products"));
            });

            return ResultTypeToCategoryStates(item, resultType);
        }

        public Task<CategoryStates> UpdateAsync(Category item)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryStates> UpdateProductAsync(Product item)
        {
            throw new NotImplementedException();
        }

        private Uri GetUri(string path)
        {
            return new Uri(Path.Combine(Properties.Resources.BaseUrlRealDataBase, $"{path}.json?auth={tokenID}"));
        } 
        
        private CategoryStates ResultTypeToCategoryStates<T>(object item,ResultType<T> resultType)
        {
            if (resultType.Success)
            {
                item.CopyPropertiesFrom(resultType.Data);
                return new CategoryStates.Success(item);
            }
            else
            {
                return new CategoryStates.Error(string.Join(Environment.NewLine, resultType.Errors));
            }
        }
    }
}
