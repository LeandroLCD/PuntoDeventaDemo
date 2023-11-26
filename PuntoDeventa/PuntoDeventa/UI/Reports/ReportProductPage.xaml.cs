using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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