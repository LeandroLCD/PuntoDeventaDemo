namespace PuntoDeventa.Domain.UseCase.CategoryProduct
{
    using PuntoDeventa.UI.CategoryProduct.Models;
    using System.Collections.Generic;
    using System.Threading;
    public interface IGetCategoryListUseCase
    {
        IAsyncEnumerable<List<Category>> Emit(CancellationToken token, int inMilliseconds = 500);
    }
}
