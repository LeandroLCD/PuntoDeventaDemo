using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PuntoDeventa.UI.Reports
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportProductPage : ContentPage
    {
        private readonly ExportReportViewModel _viewModel;

        public ReportProductPage()
        {
            InitializeComponent();
            _viewModel = (ExportReportViewModel)BindingContext;
        }

        protected override void OnAppearing()
        {
            _viewModel.OnAppearing();
            base.OnAppearing();
        }
    }
}