﻿using PuntoDeventa.Core.LocalData.DataBase;
using PuntoDeventa.Core.LocalData.DataBase.Entities;
using PuntoDeventa.Core.LocalData.Preferences;
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
        private IDataAccessObject _DAO;
        private string tokenID;

        public CategoryProductRepository()
        {
            _dataStore = DependencyService.Get<IDataStore>();
            _dataPreferences = DependencyService.Get<IDataPreferences>();
            _DAO = DependencyService.Get<IDataAccessObject>();
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

        public async IAsyncEnumerable<Category> GetAllAsync()
        {
            var resulType = await MakeCallNetwork<Dictionary<string, CategoryDTO>>(() =>
            {
                return _dataStore.GetAsync<Dictionary<string, CategoryDTO>>(GetUri("CategoryProduct"));
            });
             
            foreach (KeyValuePair<string, CategoryDTO> item in resulType.Data)
            {
                var category = new Category()
                {
                    Id = item.Key,
                    Name = item.Value.Name,
                    Products = new List<Product>()
                };
                var entity = new CategoryEntity()
                {
                    Id = item.Key,
                    Name = item.Value.Name,
                    Products = new List<ProductEntity>()
                };
                foreach (KeyValuePair<string, ProductDTO> p in item.Value.Products)
                {
                    var product = new Product();
                    product.CopyPropertiesFrom(p.Value);
                    product.Id = p.Key;
                    category.Products.Add(product);

                    var prodEntity = new ProductEntity();
                    prodEntity.CopyPropertiesFrom(product);
                    entity.Products.Add(prodEntity);
                }
                

                _DAO.InsertOrUpdate(entity);

                yield return category;
            }

        }

        public async Task<CategoryStates> GetAsync(string FireBaseId)
        {
            var resultType = await MakeCallNetwork<CategoryDTO>(() => {
                return _dataStore.GetAsync<CategoryDTO>(GetUri($"CategoryProduct/{FireBaseId}"));
            });
            //tester

            var list = _DAO.GetAll<CategoryEntity>();

            var list2  = _DAO.GetAll<ProductEntity>();

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