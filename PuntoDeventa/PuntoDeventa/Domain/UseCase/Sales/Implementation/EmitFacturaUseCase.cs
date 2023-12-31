﻿using PuntoDeventa.Data.Repository.EmissionSystem;
using PuntoDeventa.UI.Sales.Models;
using PuntoDeventa.UI.Sales.State;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PuntoDeventa.Domain.UseCase.Sales.Implementation
{
    internal class EmitFacturaUseCase : BaseSalesUseCase, IEmitFacturaUseCase
    {
        private readonly IOpenFacturaRepository _repository;

        public EmitFacturaUseCase()
        {
            _repository = DependencyService.Get<IOpenFacturaRepository>();
        }

        public async Task<SalesState> DoEmit(PaymentSales sales)
        {
            return await MakeCallUseCase(sales, async () => await _repository.EmitFactura(sales));
        }
    }
}
