using PuntoDeventa.Data.Repository.CategoryProduct;
using PuntoDeventa.Domain.UseCase.CatalogueClient;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct.Implementation
{
    internal class SyncDataUseCase : ISyncDataUseCase
    {
        private ICategoryProductRepository _repository;
        private readonly ISyncCatalogueUseCase _useCase;

        public SyncDataUseCase()
        {
            _repository = DependencyService.Get<ICategoryProductRepository>();
            _useCase = DependencyService.Get<ISyncCatalogueUseCase>();  
        }

        public void Sync(int reStarInMinutes = 10)
        {
            _repository.SyncData();
            _useCase.Sync();
            Device.StartTimer(TimeSpan.FromMinutes(reStarInMinutes), ()=> {

                _repository.SyncData();
                return true;
            });
        }
    }
}
