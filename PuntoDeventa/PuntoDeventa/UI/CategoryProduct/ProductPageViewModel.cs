using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.UseCase.CategoryProduct;
using PuntoDeventa.IU;
using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.CategoryProduct.States;
using System;
using System.Collections.Generic;
using System.Web;
using Xamarin.Forms;

namespace PuntoDeventa.UI.CategoryProduct
{
    internal class ProductPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Fields
        private IAddProductUseCase _addProductUseCase;
        private IEditProductUseCase _editProductUseCase;
        private IGetProductUseCase _getProductUseCase;
        private Product _geProduct;
        private bool _isEdit;
        private string _percentaje;
        private string _titlePage;

        #endregion

        #region Contructor
        public ProductPageViewModel()
        {

            _addProductUseCase = DependencyService.Get<IAddProductUseCase>();
            _editProductUseCase = DependencyService.Get<IEditProductUseCase>();
            _getProductUseCase = DependencyService.Get<IGetProductUseCase>();

            InicilizeCommand();

        }

        #endregion

        #region Properties



        public Product GetProduct
        {
            get
            {
                if (_geProduct.IsNull())
                {
                    _geProduct = new Product();
                }
                return _geProduct;
            }
            set
            {

                _percentaje = value.Percentage.ToString();
                NotifyPropertyChanged(nameof(Percentage));
                SetProperty(ref _geProduct, value);
            }

        }
        public bool IsEdit
        {
            get => _isEdit;
            private set => SetProperty(ref _isEdit, value);
        }

        public string Percentage
        {
            get
            {
                if (_percentaje.IsNull())
                {
                    _percentaje = GetProduct.Percentage.ToString();
                }
                return _percentaje;
            }

            private set
            {

                SetProperty(ref _percentaje, value);
            }

        }

        public string TitlePage
        {
            get => _titlePage;
            private set => SetProperty(ref _titlePage, value);
        }
        #endregion

        #region Command

        public Command<Product> AddProductCommand { get; set; }

        public Command<Product> EditProductCommand { get; set; }

        public Command BackButtonCommand { get; set; }

        public Command<Category> CategoriesChangedCommand { get; set; }

        public Command NotifyChangedCommand { get; set; }

        public Command<string> PercentageChangedCommand { get; set; }

        public Command<string> PercentageCompletedCommand { get; set; }
        #endregion

        #region Methods


        private void InicilizeCommand()
        {
            AddProductCommand = new Command<Product>(async (product) =>
            {
                IsLoading = true;
                var resp = await _addProductUseCase.Insert(product);
                switch (resp)
                {
                    case CategoryStates.Success success:
                        BackButtonCommand.Execute(null);
                        break;

                    case CategoryStates.Error error:
                        // Manejar el caso de error
                        await Shell.Current.DisplayAlert("Error", error.Message, "Ok");
                        break;

                    default:
                        break;
                }
                IsLoading = false;
            });

            EditProductCommand = new Command<Product>((product) =>
            {
                product?.Apply(async () =>
                {

                    var state = await _editProductUseCase.Edit(product);

                    switch (state)
                    {
                        case CategoryStates.Success success:
                            BackButtonCommand.Execute(null);
                            return;
                        case CategoryStates.Error error:
                            await Shell.Current.DisplayAlert("Error", error.Message, "Ok");
                            GetProduct = product;
                            return;

                    }

                });


            })
            {

            };
            BackButtonCommand = new Command(() =>
            {
                NavigationBack(typeof(ProductPage), $"CategoryId={GetProduct.CategoryId}");
            });



            PercentageChangedCommand = new Command<string>((value) =>
            {
                Console.WriteLine($"Percentage {value}");
                Percentage = value;
                if (_percentaje.Length > 0)
                    GetProduct.Percentage = float.Parse(_percentaje);
                NotifyPropertyChanged(nameof(GetProduct));

                Console.WriteLine($"_percentaje {_percentaje}");


            });

            NotifyChangedCommand = new Command(() =>
            {
                NotifyPropertyChanged(nameof(GetProduct));
            });

        }

        private async void HandlerStates(CategoryStates categoryStates)
        {
            switch (categoryStates)
            {
                case CategoryStates.Success success:
                    GetProduct = (Product)success.Data;
                    break;
                case CategoryStates.Error error:
                    await Shell.Current.DisplayAlert("Error", error.Message, "Ok");
                    await Shell.Current.Navigation.PopAsync();
                    break;
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            try
            {
                var id = HttpUtility.UrlDecode(query["ProductId"]);
                id.Apply(() =>
                   {
                       HandlerStates(_getProductUseCase.Get(id));
                       IsEdit = true;
                   });
                TitlePage = $"Editar Producto.";
                return;

            }
            catch (Exception)
            {

                var idCategory = HttpUtility.UrlDecode(query["CategoryId"]);
                idCategory.Apply(() =>
                {
                    GetProduct = new Product()
                    {
                        CategoryId = idCategory,
                    };
                    TitlePage = $"Agregar Producto.";

                });
            }
        }
        #endregion

    }
}
