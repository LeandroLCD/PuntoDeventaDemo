using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.UI.CatalogueClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogueClientPage : ContentPage
    {
        private CatalogueClientViewModel _viewModel;

        public CatalogueClientPage()
        {
            InitializeComponent();
            _viewModel = (CatalogueClientViewModel)BindingContext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnStar();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.OnStop();
        }
    }
}