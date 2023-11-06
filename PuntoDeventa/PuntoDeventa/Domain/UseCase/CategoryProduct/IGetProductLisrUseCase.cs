using PuntoDeventa.UI.CategoryProduct.Models;
using System.Collections.Generic;
using System.Threading;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct
{
    public interface IGetProductLisrUseCase
    {
        IAsyncEnumerable<List<Product>> Emit(CancellationToken token, int inMilliseconds = 180);
    }
}
