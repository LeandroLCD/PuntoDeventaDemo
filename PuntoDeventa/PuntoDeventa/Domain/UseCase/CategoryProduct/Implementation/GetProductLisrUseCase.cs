using PuntoDeventa.Data.Repository.CategoryProduct;
using PuntoDeventa.UI.CategoryProduct.Models;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct.Implementation
{
    internal class GetProductLisrUseCase : IGetProductLisrUseCase
    {
        private ICategoryProductRepository _repository;

        public GetProductLisrUseCase()
        {
            _repository = DependencyService.Get<ICategoryProductRepository>();
        }
        public async IAsyncEnumerable<List<Product>> Emit([EnumeratorCancellation] CancellationToken token, int inMilliseconds = 180)
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(inMilliseconds);
                yield return _repository.GetProductsAll();
            }

        }
    }
}
