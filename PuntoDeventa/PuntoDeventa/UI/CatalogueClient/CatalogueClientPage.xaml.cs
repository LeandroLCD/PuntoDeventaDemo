using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PuntoDeventa.UI.CatalogueClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogueClientPage : ContentPage
    {
        private CatalogueClientViewModel _viewModel;

        public CatalogueClientPage()
        {
            InitializeComponent();
            _viewModel = (CatalogueClientViewModel)BindingContext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnStar();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.OnStop();
        }
    }
}