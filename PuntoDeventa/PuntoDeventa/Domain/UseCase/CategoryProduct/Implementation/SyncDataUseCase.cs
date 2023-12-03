using PuntoDeventa.Data.Repository.CategoryProduct;
using PuntoDeventa.Domain.UseCase.CatalogueClient;
using PuntoDeventa.Domain.UseCase.Sales;
using System;
using Xamarin.Forms;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct.Implementation
{
    internal class SyncDataUseCase : ISyncDataUseCase
    {
        private readonly ICategoryProductRepository _repository;
        private readonly ISyncCatalogueUseCase _useCase;
        private readonly ISyncInformationTributaryUseCase _tributaryInformation;

        public SyncDataUseCase()
        {
            _repository = DependencyService.Get<ICategoryProductRepository>();
            _useCase = DependencyService.Get<ISyncCatalogueUseCase>();
            _tributaryInformation = DependencyService.Get<ISyncInformationTributaryUseCase>();
        }

        public void Sync(int reStarInMinutes = 10)
        {

            _tributaryInformation.Sync();
            _repository?.SyncData();
            _useCase?.Sync();
            Device.StartTimer(TimeSpan.FromMinutes(reStarInMinutes), () =>
            {

                _repository?.SyncData();
                return true;
            });
        }
    }
}
