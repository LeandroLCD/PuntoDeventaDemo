using PuntoDeventa.Domain.UseCase.CategoryProduct;
using PuntoDeventa.UI.CategoryProduct;
using Xamarin.Forms;

namespace PuntoDeventa.UI.Menu
{
    public partial class MenuAppShell : Shell
    {
        public MenuAppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            Sync();
        }
        private void Sync()
        {
            var useCase = DependencyService.Get<ISyncDataUseCase>();
            useCase.Sync();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(CategoryHome), typeof(CategoryHome));
            Routing.RegisterRoute($"//{nameof(CategoryHome)}/{nameof(CategoryDetailPage)}", typeof(CategoryDetailPage));
            Routing.RegisterRoute($"//{nameof(CategoryHome)}/{nameof(CategoryDetailPage)}/{nameof(ProductPage)}", typeof(ProductPage));
        }
    }
}