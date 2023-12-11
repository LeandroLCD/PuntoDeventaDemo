using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.UI.Reports
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportSalePage : ContentPage
    {
        private readonly ReportSalesPageViewModel _salesPageViewModel;

        public ReportSalePage()
        {
            InitializeComponent();
            _salesPageViewModel = (ReportSalesPageViewModel)BindingContext;
        }

        protected override void OnAppearing()
        {
            _salesPageViewModel.OnAppearing();
            base.OnAppearing();
        }
    }
}