using System;
using System.Collections.Generic;
using System.Text;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct
{
    public interface ISyncDataUseCase
    {
        void Sync(int reStarInMinutes = 10);
    }
}
