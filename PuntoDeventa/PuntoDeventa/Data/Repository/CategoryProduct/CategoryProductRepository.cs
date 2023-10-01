﻿namespace PuntoDeventa.Data.Repository.CategoryProduct
{
    using PuntoDeventa.Core.LocalData.DataBase;
    using PuntoDeventa.Core.LocalData.DataBase.Entities.CatalogueClient;
    using PuntoDeventa.Core.LocalData.Preferences;
    using PuntoDeventa.Core.Network;
    using PuntoDeventa.Data.DTO;
    using PuntoDeventa.Data.DTO.CatalogueProduct;
    using PuntoDeventa.Data.Mappers;
    using PuntoDeventa.Domain.Helpers;
    using PuntoDeventa.Domain.Models;
    using PuntoDeventa.UI.CategoryProduct.Models;
    using PuntoDeventa.UI.CategoryProduct.States;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Xamarin.Forms.Internals;
    internal class CategoryProductRepository : BaseRepository, ICategoryProductRepository
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
            var resultType = await MakeCallNetwork<Category>(() =>
            {
                return _dataStore.DeleteAsync<Category>(GetUri($"CategoryProduct/{item.Id}"));
            });

            return ResultTypeToCategoryStates(OperationDTO.Delete, item.ToCategoryEntity(), resultType);

        }

        public async Task<CategoryStates> DeleteProdctAsync(Product item)
        {
            var resultType = await MakeCallNetwork<Product>(() =>
            {
                return _dataStore.DeleteAsync<Product>(GetUri($"CategoryProduct/{item.CategoryId}/Products/{item.Id}"));
            });

            return ResultTypeToCategoryStates(OperationDTO.Delete, item.ToProductDTO(), resultType);
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
            var resultType = await MakeCallNetwork<CategoryDTO>(() =>
            {
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
            var resultType = await MakeCallNetwork<CategoryDTO>(() =>
            {
                return _dataStore.PostAsync(item, GetUri($"CategoryProduct"));
            });

            return ResultTypeToCategoryStates(OperationDTO.InsertOrUpdate, item.ToCategoryEntity(), resultType);
        }

        public async Task<CategoryStates> InsertProductAsync(Product item)
        {
            var resultType = await MakeCallNetwork<ProductDTO>(() =>
            {
                return _dataStore.PostAsync(item, GetUri($"CategoryProduct/{item.CategoryId}/Products"));
            });

            return ResultTypeToCategoryStates(OperationDTO.InsertOrUpdate, item.ToProductEntity(), resultType);
        }

        public Task<CategoryStates> UpdateAsync(Category item)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryStates> UpdateProductAsync(Product item)
        {

            var resultType = await MakeCallNetwork<ProductDTO>(() =>
            {
                return _dataStore.PutAsync(item.ToProductDTO(), GetUri($"CategoryProduct/{item.CategoryId}/Products/{item.Id}"));
            });

            var entity = item.ToProductEntity();
            entity.Category = _DAO.Get<CategoryEntity>(item.CategoryId);

            return ResultTypeToCategoryStates(OperationDTO.InsertOrUpdate, entity, resultType);
        }

        public List<Category> GetAll()
        {
            return _DAO.GetAll<CategoryEntity>()?.Select(c => new Category()
            {
                Brand = c.Brand,
                Id = c.Id,
                Name = c.Name,
                Products = c.Products?.Select(p => p.ToProduct()).ToList()
            }).ToList();
        }

        public List<Product> GetProductsAll()
        {
            return _DAO.GetAll<ProductEntity>()?.Select(p => p.ToProduct()).ToList();
        }

        public void SyncData()
        {
            _dataPreferences.GetUserData()?.Apply(async () =>
            {
                var resulType = await MakeCallNetwork<Dictionary<string, CategoryDTO>>(() =>
                {
                    return _dataStore.GetAsync<Dictionary<string, CategoryDTO>>(GetUri("CategoryProduct"));
                });
                if(resulType.Success)
                {
                    foreach (KeyValuePair<string, CategoryDTO> item in resulType.Data)
                    {
                        var entity = new CategoryEntity()
                        {
                            Id = item.Key,
                            Name = item.Value.Name,
                            Brand = item.Value.Brand,
                            Products = item.Value.Products?.Select(p => p.Value.ToProductEntity(p.Key, item.Key)).ToList(),
                        };
                        _DAO.InsertOrUpdate(entity);
                    }

                }
                

            });

        }

        private Uri GetUri(string path)
        {
            return new Uri(Path.Combine(Properties.Resources.BaseUrlRealDataBase, $"{path}.json?auth={tokenID}"));
        }

        private CategoryStates ResultTypeToCategoryStates<T>(OperationDTO method, object itemEntity, ResultType<T> resultType)
        {
            if (resultType.Success)
            {
                itemEntity.CopyPropertiesFrom(resultType.Data);
                switch (method)
                {
                    case OperationDTO.Delete:
                        _DAO.Delete(itemEntity);
                        break;
                    default:
                        _DAO.InsertOrUpdate(itemEntity);
                        break;
                }
                var category = Activator.CreateInstance(typeof(T));
                category.CopyPropertiesFrom(resultType.Data);
                return new CategoryStates.Success(category);
            }
            else
            {
                return new CategoryStates.Error(string.Join(Environment.NewLine, resultType.Errors));
            }
        }

        public CategoryStates GetCategory(string id)
        {

            var categoryEntity = _DAO.Get<CategoryEntity>(id);
            var product = _DAO.GetAll<ProductEntity>().Where(p => p.CategoryId == id);
            if (categoryEntity.IsNotNull())
            {
                var category = new Category()
                {
                    Name = categoryEntity.Name,
                    Id = categoryEntity.Id,
                    Brand = categoryEntity.Brand,
                    Products = categoryEntity.Products.Select(p => p.ToProduct()).ToList(),
                };
                return new CategoryStates.Success(category);
            }
            return new CategoryStates.Error("Categoria no encontrada");
        }

        public CategoryStates GetProducts(string id)
        {
            var productEntity = _DAO.Get<ProductEntity>(id);
            if (productEntity.IsNotNull())
            {

                return new CategoryStates.Success(productEntity.ToProduct());
            }
            return new CategoryStates.Error("Producto no encontrado");
        }
    }
}
