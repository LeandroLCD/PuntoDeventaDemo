using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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