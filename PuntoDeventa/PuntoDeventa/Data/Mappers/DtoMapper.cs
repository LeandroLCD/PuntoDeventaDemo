using PuntoDeventa.Data.DTO;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.Models;
using System;

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



        //public static CategoryDTO ToCategoryDTO(this Category model)
        //{
        //    if (model.IsNotNull())
        //    {
        //        Dictionary<string, ProductDTO> product = new Dictionary<string, ProductDTO>();
        //        model.Products?.ForEach(item =>
        //        {
        //            product.Add(item.Id, new ProductDTO()
        //            {
        //                Name = item.Name,
        //                BarCode = item.BarCode,
        //                Code = item.Code,
        //                Description = item.Description,
        //                UDM = item.UDM,
        //                IsOffer = item.IsOffer,
        //                Percentage = item.Percentage,
        //                PriceGross = item.PriceGross,
        //            });
        //        });
        //        return new CategoryDTO()
        //        {
        //            Name = model.Name,
        //            Brand = model.Brand,
        //            Products = product.Count > 0 ? product : null,

        //        };
        //    }

        //    else
        //        return null;
        //}

        //public static ProductDTO ToProductDTO(this Product model)
        //{
        //    if (model.IsNotNull())
        //    {
        //        ProductDTO DTO = new ProductDTO();
        //        DTO.CopyPropertiesFrom(model);
        //        return DTO;
        //    }

        //    else
        //        return null;
        //}

    }
}
