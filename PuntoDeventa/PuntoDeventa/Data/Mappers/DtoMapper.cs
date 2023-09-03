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
    }
}
