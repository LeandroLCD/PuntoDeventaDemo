using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.UI.CategoryProduct
{
    [QueryProperty(nameof(LocalID), "CategoryId")]
    public partial class CategoryDetailPage : ContentPage
    {
        private CategoryDetailPageViewModel _viewModel;
        private string _localId;

        public string LocalID
        {
            set
            {
                LoadData(value);
            }
        }

        private void LoadData(string value)
        {
            _localId = value;
        }

        public CategoryDetailPage()
        {
            _viewModel = (CategoryDetailPageViewModel)BindingContext;
            InitializeComponent();
        }
    }
}