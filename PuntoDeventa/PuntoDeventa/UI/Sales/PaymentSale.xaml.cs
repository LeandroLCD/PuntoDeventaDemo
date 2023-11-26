using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PuntoDeventa.UI.Sales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentSale : ContentPage
    {
        private readonly PaymentPageViewModel _viewModel;

        public PaymentSale()
        {
            InitializeComponent();
            _viewModel = (PaymentPageViewModel)BindingContext;
        }

        protected override void OnAppearing()
        {
            _viewModel.OnAppernig(ContentGrid);
            base.OnAppearing();
        }
    }
}