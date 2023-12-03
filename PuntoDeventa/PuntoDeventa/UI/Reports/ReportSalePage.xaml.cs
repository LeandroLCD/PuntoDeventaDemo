using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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