using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.UI.Reports
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportProductPage : ContentPage
    {
        private readonly ReportProductPageViewModel _viewModel;

        public ReportProductPage()
        {
            InitializeComponent();
            _viewModel = (ReportProductPageViewModel)BindingContext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}