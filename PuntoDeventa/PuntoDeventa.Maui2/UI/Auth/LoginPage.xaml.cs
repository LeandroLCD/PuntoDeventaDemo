using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.UI.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly LoginPageViewModel _viewModel;

        public LoginPage()
        {
            _viewModel = DependencyService.Get<LoginPageViewModel>();
            BindingContext = _viewModel;
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnApperning(GridParent);
        }
    }
}