using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PuntoDeventa.UI.CategoryProduct
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryHome : ContentPage
    {
        private CategoryHomeViewModel _viewModel;

        public CategoryHome()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel = (CategoryHomeViewModel)BindingContext;
            _viewModel.OnApperning();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.OnStop();
        }

    }
}