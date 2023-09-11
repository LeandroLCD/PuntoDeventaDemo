using PuntoDeventa.Data.Repository.CategoryProduct;
using System;
using Xamarin.Forms;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct.Implementation
{
    internal class SyncDataUseCase : ISyncDataUseCase
    {
        private ICategoryProductRepository _repository;

        public SyncDataUseCase()
        {
            _repository = DependencyService.Get<ICategoryProductRepository>();
        }

        public void Sync(int reStarInMinutes = 10)
        {
            _repository.SyncDataAsync();
            Device.StartTimer(TimeSpan.FromMinutes(reStarInMinutes), ()=> {

                _repository.SyncDataAsync();

                return true;
            });
        }
    }
}
