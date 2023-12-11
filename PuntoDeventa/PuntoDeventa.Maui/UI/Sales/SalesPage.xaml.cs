using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.UI.Sales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesPage : ContentPage
    {
        private readonly SalesPageViewModel _viewModel;

        public SalesPage()
        {
            InitializeComponent();
            _viewModel = (SalesPageViewModel)BindingContext;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnStart();

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.OnStop();
        }


    }
}