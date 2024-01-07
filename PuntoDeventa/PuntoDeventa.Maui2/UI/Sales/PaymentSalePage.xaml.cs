using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.UI.Sales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentSalePage : ContentPage
    {
        private readonly PaymentPageViewModel _viewModel;

        public PaymentSalePage()
        {
            InitializeComponent();
            _viewModel = (PaymentPageViewModel)BindingContext;
        }

        protected override void OnAppearing()
        {
            _viewModel.OnAppearing(ContentGrid);
            base.OnAppearing();
        }
    }
}