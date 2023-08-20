using PuntoDeventa.Data.DTO;
using PuntoDeventa.Domain.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuntoDeventa.Data.Mappers
{
    public static class DtoMapper
    {
        public static UserData ToUserData(this UserDataDTO dto)
        {
            return new UserData() 
            { 
            DateLogin = DateTime.Now,
            DisplayName = dto.DisplayName,
            Email = dto.Email,
            IdToken = dto.IdToken,
            LocalId = dto.LocalId,
            };
        }
    }
}
