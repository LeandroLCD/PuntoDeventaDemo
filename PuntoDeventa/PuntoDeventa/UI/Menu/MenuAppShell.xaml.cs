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
        }

        private void RegisterRoutes()
        {
             Routing.RegisterRoute(nameof(CategoryHome), typeof(CategoryHome));
            Routing.RegisterRoute($"//{nameof(CategoryHome)}/{nameof(CategoryDetailPage)}", typeof(CategoryDetailPage));
        }
    }
}