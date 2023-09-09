using PuntoDeventa.Core.LocalData.DataBase.Entities;
using PuntoDeventa.Data.DTO;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.Models;
using PuntoDeventa.UI.CategoryProduct.Models;
using System;
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
                    DateLogin = DateTime.Now,
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
                    PriceGross= model.PriceGross,
                    Sku = model.Code,
                    CategoryId = model.CategoryId
                };
            }

            else
                return null;
        }

    }
}
