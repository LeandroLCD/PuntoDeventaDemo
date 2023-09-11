using PuntoDeventa.UI.CategoryProduct.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct
{
    public interface IGetProductLisrUseCase
    {
        IAsyncEnumerable<List<Product>> Emit(CancellationToken token, int inMilliseconds = 180);
    }
}
