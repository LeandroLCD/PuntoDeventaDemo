using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PuntoDeventa.UI.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private LoginPageViewModel _viewModel;

        public LoginPage()
        {
            _viewModel = DependencyService.Get<LoginPageViewModel>();
            BindingContext = _viewModel;
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //var user = _viewModel.OnAppearing();
        }
    }
}