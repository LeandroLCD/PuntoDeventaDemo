using Newtonsoft.Json;
using PuntoDeventa.Data.Models;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.UseCase.Sales;
using PuntoDeventa.IU;
using PuntoDeventa.UI.Sales.Models;
using PuntoDeventa.UI.Sales.Screen;
using PuntoDeventa.UI.Sales.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using static PuntoDeventa.UI.Sales.State.ScreenStates;

namespace PuntoDeventa.UI.Sales
{
    internal class PaymentPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Fields
        private readonly IPreViewPdf _previewUseCase;
        private readonly IEmitFacturaUseCase _emitFacturaUseCase;
        private readonly IEmitNotaDePedidoUseCase _emitNotaDePedidoUseCase;
        private Dictionary<PaymentType, Payment> _dictionaryPay;
        private int _totalPay;
        private const double Tolerance = 1;
        #endregion



        public PaymentPageViewModel()
        {
            _previewUseCase = DependencyService.Get<IPreViewPdf>();

            _emitFacturaUseCase = DependencyService.Get<IEmitFacturaUseCase>();

            _emitNotaDePedidoUseCase = DependencyService.Get<IEmitNotaDePedidoUseCase>();

            InitializeCommand();
        }


        #region Properties
        public Dictionary<PaymentType, Payment> PaymentList
        {
            get
            {
                if (_dictionaryPay.IsNull())
                    _dictionaryPay = new Dictionary<PaymentType, Payment>();
                return _dictionaryPay;
            }
            set => SetProperty(ref _dictionaryPay, value);
        }


        public int TotalPay
        {
            get => _totalPay;
            set => SetProperty(ref _totalPay, value);
        }
        private ScreenStates CurrentScreenStates { get; set; }
        private PaymentSales PaymentSales { get; set; }
        private Grid Content { get; set; }

        #endregion

        #region Command
        public Command<int> CastCommand { get; set; }
        public Command<int> CheckCommand { get; set; }
        public Command<int> TransferCommand { get; set; }
        public Command<int> DepositCommand { get; set; }
        public Command PreviewCommand { get; set; }
        public Command BackButtonCommand { get; set; }


        #endregion

        private void InitializeCommand()
        {
            CastCommand = new Command<int>((value) =>
            {
                AddPayment(PaymentType.Cash, value);
            });

            DepositCommand = new Command<int>((value) =>
            {
                AddPayment(PaymentType.BankDeposit, value);
            });

            TransferCommand = new Command<int>((value) =>
            {
                AddPayment(PaymentType.BankTransfer, value);
            });

            CheckCommand = new Command<int>((value) =>
            {
                AddPayment(PaymentType.BankCheck, value);
            });

            BackButtonCommand = new Command(() =>
            {
                var state = CurrentScreenStates.BackState();
                switch (state)
                {
                    case null:
                        NavigationBack(typeof(PaymentSalePage), "isNew=false");
                        break;
                    case Success success:
                        NavigationBack(typeof(PaymentSalePage), "isNew=true");
                        break;
                    default:
                        HandlerScreenState(state);
                        break;
                }


            });

            PreviewCommand = new Command(async () =>
            {

                var state = await _previewUseCase.ToCreate(PaymentSales);
                switch (state)
                {
                    case SalesState.Error error:
                        break;
                    case SalesState.Success success:

                        HandlerScreenState(Preview.Instance(success.Data.ToString()));
                        break;
                }

            });
        }

        private void AddPayment(PaymentType type, int value)
        {

            if (PaymentList.ContainsKey(type))
                PaymentList[type] = new Payment(value, type);
            else
            {
                PaymentList.Add(type, new Payment(value, type));
            }

            TotalPay = PaymentList.Sum(p => p.Value.Amount);
        }

        public void OnAppearing(Grid view)
        {
            Content = view;
            HandlerScreenState(DocumentSelection.Instance);
        }

        private async void CreditEmit()
        {

            PaymentSales.PaymentMethod = PaymentMethod.Credit;
            var paymentSalesPaymentTypes = PaymentList.Select(p => p.Value).ToList();

            if (paymentSalesPaymentTypes.Any())
                PaymentSales.PaymentTypes = paymentSalesPaymentTypes.Where(p => p.Amount > 0);

            //SalesState state;
            //if (PaymentSales.DocumentType.Equals(DteType.Factura))
            //    state = await _emitFacturaUseCase.DoEmit(PaymentSales);
            //else
            //    state = await _emitNotaDePedidoUseCase.DoEmit(PaymentSales);

            var state = PaymentSales.DocumentType == DteType.Factura
                ? await _emitFacturaUseCase.DoEmit(PaymentSales)
                : await _emitNotaDePedidoUseCase.DoEmit(PaymentSales);


            switch (state)
            {
                case SalesState.Error error:
                    await Shell.Current.DisplayToastAsync(string.Join(Environment.NewLine, error));
                    break;
                case SalesState.Success success:
                    HandlerScreenState(Success.Instance(success.Data.ToString()));
                    break;
            }


        }

        private async void CaskEmit()
        {
            PaymentSales.PaymentMethod = PaymentMethod.Counted;

            var paymentList = PaymentList.Select(p => p.Value).ToList();
            var total = PaymentSales.Sale.Products.Sum(s => Math.Floor(s.SubTotal * (1 + s.Vat)));
            var pay = paymentList.Sum(p => p.Amount);


            if (Math.Abs(total - pay) < Tolerance)
            {
                PaymentSales.PaymentTypes = PaymentList.Select(p => p.Value).Where(p => p.Amount > 0);

                PaymentSales.PaymentTypes = paymentList;

                var state = PaymentSales.DocumentType == DteType.Factura
                    ? await _emitFacturaUseCase.DoEmit(PaymentSales)
                    : await _emitNotaDePedidoUseCase.DoEmit(PaymentSales);

                switch (state)
                {
                    case SalesState.Error error:
                        await Shell.Current.DisplayToastAsync(string.Join(Environment.NewLine, error));
                        break;
                    case SalesState.Success success:
                        HandlerScreenState(Success.Instance(success.Data.ToString()));
                        break;
                }

            }
            else
            {
                await Shell.Current.DisplaySnackBarAsync(
                    $"El monto {total:C2}, ingresado no es igual al monto total de la venta {total:C2}",
                    "Aceptar", action: () => Task.CompletedTask,
                    duration: TimeSpan.FromSeconds(10));
            }
        }

        private void HandlerScreenState(ScreenStates state)
        {
            CurrentScreenStates = state;
            switch (state)
            {
                case Preview preview:
                    Content.Children.Clear();
                    Content.Children.Add(new PdfViewScreen(
                        preview.PdfStream, preview.PathPdf,
                        new Command(() => HandlerScreenState(DocumentSelection.Instance)),
                        "Pagar"));

                    break;
                case DocumentSelection documentSelection:
                    Content.Children.Clear();
                    Content.Children.Add(new DocumentTypeScreen(
                        new Command<object>((type) =>
                        {
                            PaymentSales.DocumentType = (DteType)type;
                            HandlerScreenState(PaymentSelection.Instance);
                        })));
                    break;

                case PaymentSelection paymentSelection:
                    Content.Children.Clear();
                    Content.Children.Add(new PaymentListScreen(
                        new Command(CaskEmit),
                        commandCredit: new Command(CreditEmit),
                        CastCommand, CheckCommand, DepositCommand, TransferCommand));
                    break;
                case Success success:
                    Content.Children.Clear();
                    Content.Children.Add(new PdfViewScreen(success.PdfStream, success.PathPdf, new Command(() =>
                    {
                        BackButtonCommand.Execute(null);
                    })));
                    break;
            }
        }

        public async void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            try
            {
                var saleDecode = HttpUtility.UrlDecode(query["Sale"]);
                HandlerParameter(JsonConvert.DeserializeObject<Sale>(saleDecode));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private void HandlerParameter(Sale sale)
        {
            if (sale.IsNotNull())
            {
                PaymentSales = new PaymentSales()
                {
                    Sale = sale
                };
            }
        }
    }
}
