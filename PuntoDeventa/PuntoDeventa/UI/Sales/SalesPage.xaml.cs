using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PuntoDeventa.UI.Sales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesPage : ContentPage
    {
        private SalesPageViewModel _viewModel;

        public SalesPage()
        {
            InitializeComponent();
            _viewModel = (SalesPageViewModel)BindingContext;
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