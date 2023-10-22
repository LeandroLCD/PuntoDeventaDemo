using PuntoDeventa.Core.LocalData.DataBase.Entities.CatalogueClient;
using PuntoDeventa.Data.DTO.Auth;
using PuntoDeventa.Data.DTO.CatalogueClient;
using PuntoDeventa.Data.DTO.CatalogueProduct;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.Models;
using PuntoDeventa.UI.CatalogueClient.Model;
using PuntoDeventa.UI.CategoryProduct.Models;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace PuntoDeventa.Data.Mappers
{
    public static class DtoMapper
    {
        public static UserData ToUserData(this UserDataDTO dto)
        {
            if (dto.IsNotNull())
                return new UserData()
                {
                    DateLogin = dto.DateLogin,
                    DisplayName = dto.DisplayName,
                    Email = dto.Email,
                    IdToken = dto.IdToken,
                    LocalId = dto.LocalId,
                };
            else
                return null;
        }
        public static RemembermeUser ToRemembermeUser(this RemembermeUserDTO dto)
        {
            if (dto.IsNotNull())
                return new RemembermeUser(dto.Email, dto.IsRememberme);
            else
                return null;
        }

        public static RemembermeUserDTO ToRemembermeUserDTO(this RemembermeUser dto)
        {
            if (dto.IsNotNull())
                return new RemembermeUserDTO(dto.Email, dto.IsRememberme);
            else
                return null;
        }



        public static CategoryDTO ToCategoryDTO(this Category model)
        {
            if (model.IsNotNull())
            {
                Dictionary<string, ProductDTO> product = new Dictionary<string, ProductDTO>();
                model.Products?.ForEach(item =>
                {
                    product.Add(item.Id, new ProductDTO()
                    {
                        Name = item.Name,
                        BarCode = item.BarCode,
                        Sku = item.Sku,
                        Description = item.Description,
                        UDM = item.UDM,
                        IsOffer = item.IsOffer,
                        Percentage = item.Percentage,
                        PriceGross = item.PriceGross,
                    });
                });
                return new CategoryDTO()
                {
                    Name = model.Name,
                    Brand = model.Brand,
                    Products = product.Count > 0 ? product : null,

                };
            }

            else
                return null;
        }

        public static Category ToCategory(this CategoryDTO model, string fireBaseId)
        {
            if (model.IsNotNull())
            {
                var product = new List<Product>();
                model.Products?.ForEach(item =>
                {
                    product.Add(new Product()
                    {
                        Name = item.Value.Name,
                        BarCode = item.Value.BarCode,
                        Sku = item.Value.Sku,
                        Description = item.Value.Description,
                        UDM = item.Value.UDM,
                        IsOffer = item.Value.IsOffer,
                        Percentage = item.Value.Percentage,
                        PriceGross = item.Value.PriceGross,
                        Id = item.Key
                    });
                });
                return new Category()
                {
                    Id = fireBaseId,
                    Name = model.Name,
                    Brand = model.Brand,
                    Products = product.Count > 0 ? product : null,

                };
            }

            else
                return null;
        }

        public static Category ToCategory(this CategoryDTO model)
        {
            if (model.IsNotNull())
            {
                var category = new Category();
                category.CopyPropertiesFrom(model);
                return category;
            }

            else
                return null;
        }

        public static ProductDTO ToProductDTO(this Product model)
        {
            if (model.IsNotNull())
            {
                ProductDTO DTO = new ProductDTO();
                DTO.CopyPropertiesFrom(model);
                return DTO;
            }

            else
                return null;
        }

        public static Product ToProduct(this ProductEntity model)
        {
            if (model.IsNotNull())
            {

                return new Product()
                {
                    Name = model.Name,
                    Id = model.Id,
                    IsOffer = model.IsOffer,
                    BarCode = model.BarCode,
                    Description = model.Description,
                    UDM = model.UDM,
                    Percentage = model.Percentage,
                    PriceGross = model.PriceGross,
                    Sku = model.Code,
                    CategoryId = model.CategoryId,
                    InReport = model.InReport,
                };
            }

            else
                return null;
        }

        public static ProductEntity ToProductEntity(this ProductDTO model, string productId, string categoryId)
        {
            if (model.IsNotNull())
            {

                return new ProductEntity()
                {
                    Name = model.Name,
                    Id = productId,
                    IsOffer = model.IsOffer,
                    BarCode = model.BarCode,
                    Description = model.Description,
                    UDM = model.UDM,
                    Percentage = model.Percentage,
                    PriceGross = model.PriceGross,
                    Code = model.Sku,
                    CategoryId = categoryId,
                    InReport = model.InReport
                };
            }

            else
                return null;
        }
        public static ProductEntity ToProductEntity(this Product model)
        {
            if (model.IsNotNull())
            {

                var entity = new ProductEntity();
                entity.CopyPropertiesFrom(model);
                return entity;
            }

            else
                return null;
        }

        public static CategoryEntity ToCategoryEntity(this Category model)
        {
            if (model.IsNotNull())
            {
                List<ProductEntity> product = new List<ProductEntity>();
                model.Products?.ForEach(item =>
                {
                    product.Add(new ProductEntity()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        BarCode = item.BarCode,
                        Code = item.Sku,
                        Description = item.Description,
                        UDM = item.UDM,
                        IsOffer = item.IsOffer,
                        Percentage = item.Percentage,
                        PriceGross = item.PriceGross,
                    });
                });
                return new CategoryEntity()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Brand = model.Brand,
                    Products = product.Count > 0 ? product : null,

                };
            }

            else
                return null;
        }

        public static Client ToClient(this ClientEntity model)
        {
            if (model.IsNotNull())
            {
                var client = new Client();
                client.CopyPropertiesFrom(model);
                return client;
            }

            else
                return null;
        }

        public static ClientEntity ToClientEntity(this Client model)
        {
            if (model.IsNotNull())
            {
                var client = new ClientEntity();
                client.CopyPropertiesFrom(model);
                return client;
            }

            else
                return null;
        }

        public static ClientDTO ToClientDTO(this Client model)
        {
            if (model.IsNotNull())
            {
                var client = new ClientDTO();
                client.CopyPropertiesFrom(model);
                return client;
            }

            else
                return null;
        }

        public static SalesRoutesEntity ToSalesRoutesEntity(this SalesRoutes model)
        {
            if (model.IsNotNull())
            {
                var route = new SalesRoutesEntity();
                route.CopyPropertiesFrom(model);
                return route;
            }

            else
                return null;
        }

    }
}
