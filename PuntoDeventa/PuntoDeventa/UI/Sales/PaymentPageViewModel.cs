using Newtonsoft.Json;
using PuntoDeventa.Data.Models;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.UseCase.Sales;
using PuntoDeventa.IU;
using PuntoDeventa.UI.Controls.Animations;
using PuntoDeventa.UI.Sales.Models;
using PuntoDeventa.UI.Sales.Screen;
using PuntoDeventa.UI.Sales.State;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace PuntoDeventa.UI.Sales
{
    internal class PaymentPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Fields
        private readonly IPreViewPdf _previewUseCase;
        private readonly IEmitFacturaUseCase _emitFacturaUseCase;
        private readonly IEmitFacturaUseCase _emitNotaDePedidoUseCase;
        private Dictionary<string, Payment> _dictionaryPay;
        const double Tolerance = 1;
        #endregion

        public PaymentPageViewModel()
        {
            _previewUseCase = DependencyService.Get<IPreViewPdf>();

            _emitFacturaUseCase = DependencyService.Get<IEmitFacturaUseCase>();

            _emitNotaDePedidoUseCase = DependencyService.Get<IEmitFacturaUseCase>();

            CastCommand = new Command<object>((obj) =>
            {
                var payment = (KeyValuePair<string, double>)obj;
                object key = null;
                switch (payment.Key)
                {
                    case nameof(PaymentType.Cash):
                        key = PaymentType.Cash;
                        break;
                    case nameof(PaymentType.BankCheck):
                        key = PaymentType.BankCheck;
                        break;
                    case nameof(PaymentType.BankTransfer):
                        key = PaymentType.BankTransfer;
                        break;
                    case nameof(PaymentType.BankDeposit):
                        key = PaymentType.BankDeposit;
                        break;

                }
                if (PaymentList.ContainsKey(payment.Key))
                    PaymentList[payment.Key] = new Payment(payment.Value, (PaymentType)key);
                else
                {
                    PaymentList.Add(payment.Key, new Payment(payment.Value, (PaymentType)key));
                }
            });
        }

        private PaymentSales PaymentSales { get; set; }
        private Grid Content { get; set; }

        public Command<object> CastCommand { get; set; }

        public Dictionary<string, Payment> PaymentList
        {
            get
            {
                if (_dictionaryPay.IsNull())
                    _dictionaryPay = new Dictionary<string, Payment>();
                return _dictionaryPay;
            }
            set => SetProperty(ref _dictionaryPay, value);
        }

        private ScreenStates ScreenStates { get; set; }

        public bool ChangedPayment(PaymentType key, double value)
        {

            return true;
        }
        public void OnAppernig(Grid view)
        {
            Content = view;
            HandlerScreenState(ScreenStates.DocumentSelection.Instance);
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
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private void HandlerScreenState(ScreenStates state)
        {
            switch (state)
            {
                case ScreenStates.Preview preview:
                    ScreenStates = preview;
                    Content.ClearAllAnimate();
                    Content.Children.Add(new PdfViewScreen(
                        preview.PdfStream,
                        new Command(() => HandlerScreenState(ScreenStates.DocumentSelection.Instance)),
                        "Pagar"));
                    break;
                case ScreenStates.DocumentSelection documentSelection:
                    // Content.ClearAllAnimate();
                    Content.Children.Add(new DocumentTypeScreen(
                        new Command<object>((type) =>
                        {
                            PaymentSales.DocumentType = (DteType)type;
                            HandlerScreenState(ScreenStates.PaymentSelection.Instance);
                        })));
                    ScreenStates = documentSelection;
                    break;
                case ScreenStates.PaymentSelection paymentSelection:
                    Content.Children.Clear();
                    Content.Children.Add(new PaymentListScreen(
                        new Command<Dictionary<PaymentType, Payment>>(CaskEmit),
                        commandCredit: new Command<Dictionary<PaymentType, Payment>>(CreditEmit),
                        CastCommand));
                    break;
                case ScreenStates.Success success:
                    ScreenStates = success;
                    Content.Children.Clear();
                    Content.Children.Add(new PdfViewScreen(success.PdfStream, new Command(async () =>
                    {
                        //Como indicamos a la vista que debe borrar los datos?
                        await Shell.Current.GoToAsync($"..");
                    })));
                    break;
            }
        }

        private async void CreditEmit(Dictionary<PaymentType, Payment> paymentList)
        {
            PaymentSales.PaymentMethod = PaymentMethod.Credit;
            var paymentSalesPaymentTypes = paymentList.Select(p => p.Value).ToList();

            if (paymentSalesPaymentTypes.Any())
                PaymentSales.PaymentTypes = paymentSalesPaymentTypes.Where(p => p.Amount > 0);

            var state = PaymentSales.DocumentType == DteType.Factura
                ? await _emitFacturaUseCase.DoEmit(PaymentSales)
                : await _emitNotaDePedidoUseCase.DoEmit(PaymentSales);
            switch (state)
            {
                case SalesState.Error error:
                    await Shell.Current.DisplayToastAsync(string.Join(Environment.NewLine, error));
                    break;
                case SalesState.Success success:
                    HandlerScreenState(ScreenStates.Success.Instance((string)success.Data));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state));
            }


        }

        private async void CaskEmit(Dictionary<PaymentType, Payment> paymentList)
        {
            PaymentSales.PaymentMethod = PaymentMethod.Counted;
            var paymentSalesPaymentTypes = paymentList.Select(p => p.Value).ToList();
            var total = PaymentSales.Sale.Products.Sum(s => s.SubTotal * (1 + s.Vat));
            var pay = paymentSalesPaymentTypes.Sum(p => p.Amount);

            if (paymentSalesPaymentTypes.Any())
                PaymentSales.PaymentTypes = paymentSalesPaymentTypes.Where(p => p.Amount > 0);


            if (Math.Abs(PaymentSales.Sale.Products.Sum(s => s.SubTotal * s.Vat) - paymentSalesPaymentTypes.Sum(p => p.Amount)) < Tolerance)
            {
                Console.WriteLine($"factura:{total}, pagado:-{pay}");
                PaymentSales.PaymentTypes = paymentSalesPaymentTypes;
                var state = PaymentSales.DocumentType == DteType.Factura
                    ? await _emitFacturaUseCase.DoEmit(PaymentSales)
                    : await _emitNotaDePedidoUseCase.DoEmit(PaymentSales);
                switch (state)
                {
                    case SalesState.Error error:
                        await Shell.Current.DisplayToastAsync(string.Join(Environment.NewLine, error));
                        break;
                    case SalesState.Success success:
                        HandlerScreenState(ScreenStates.Success.Instance(new StreamReader((string)success.Data).BaseStream));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(state));
                }

            }
            else
            {
                await Shell.Current.DisplaySnackBarAsync(
                    $"El monto ingresado no es igual, al total de la venta {total:C0}",
                    "Aceptar", action: () => Task.CompletedTask,
                    duration: TimeSpan.FromSeconds(10));
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
