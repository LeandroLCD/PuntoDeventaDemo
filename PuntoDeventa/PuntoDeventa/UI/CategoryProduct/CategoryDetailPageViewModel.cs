﻿using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.UseCase.CategoryProduct;
using PuntoDeventa.IU;
using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.CategoryProduct.States;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Web;
using Xamarin.Forms;

namespace PuntoDeventa.UI.CategoryProduct
{
    internal class CategoryDetailPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Fields
        private ObservableCollection<Product> _productList;
        private IAddProductUseCase _addCProductUseCase;
        private IGetCategoryUseCase _getCategoryUseCase;
        private string _searchText;
        private Category _getCategory;
        private string _title;

        #endregion

        #region Contructor
        public CategoryDetailPageViewModel()
        {
            InicilizeCommand();

            _addCProductUseCase = DependencyService.Get<IAddProductUseCase>();
            _getCategoryUseCase = DependencyService.Get<IGetCategoryUseCase>();
            TokenSource = new CancellationTokenSource();
        }

        #endregion

        #region Properties

        public ObservableCollection<Product> ProductsList
        {
            get
            {
                if (_productList.IsNull())
                {
                    _productList = new ObservableCollection<Product>();
                }
                return _productList;
            }
            set => SetProperty(ref _productList, value);
        }
        public string SearchText
        {
            get => _searchText;
            private set => SetProperty(ref _searchText, value);
        }

        public Category GetCategory
        {
            get => _getCategory;
            private set
            {
                if (value.ProductCount > 0)
                    ProductsList = new ObservableCollection<Product>(value.Products);

                SetProperty(ref _getCategory, value);
            }
        }

        public string TitlePage
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private CancellationTokenSource TokenSource { set; get; }

        #endregion

        #region Command

        public Command<string> SearchBarCommand { get; set; }

        public Command<Product> ProductChangedCommand { get; set; }

        public Command NewProductCommand { get; set; }
        #endregion

        #region Methods

        public void OnApperning()
        {
            InicializeProperties();
        }

        public void OnStop()
        {
            TokenSource.Cancel();
            TokenSource.Dispose();
        }
        private void InicializeProperties()
        {

            ProductsList = new ObservableCollection<Product>();
        }

        private void InicilizeCommand()
        {
            SearchBarCommand = new Command<string>((test) =>
            {
                SetProductList(test);
                _searchText = test;
            });

            ProductChangedCommand = new Command<Product>(async (product) =>
            {
                if (product.IsNotNull())
                {
                    await Shell.Current.GoToAsync($"{nameof(ProductPage)}?ProductId={product.Id}&CategoryId={product.CategoryId}", true);
                }
            });

            NewProductCommand = new Command(async () =>
            {

                await Shell.Current.GoToAsync($"{nameof(ProductPage)}?CategoryId={GetCategory.Id}", true);

            });
        }

        private async void HandlerStates(CategoryStates categoryStates)
        {
            switch (categoryStates)
            {
                case CategoryStates.Success success:
                    GetCategory = (Category)success.Data;
                    _title = GetCategory == null ? "S/Ruta" : GetCategory.Name;
                    break;
                case CategoryStates.Error error:
                    await Shell.Current.DisplayAlert("Error", error.Message, "Ok");
                    break;
            }
        }

        private void SetProductList(string name = null)
        {
            if (name.IsNotNull())
                ProductsList = new ObservableCollection<Product>(GetCategory.Products?.Where(c => c.Name.ToLower().Contains(name.ToLower())));
            else
                ProductsList = new ObservableCollection<Product>(GetCategory.Products);
        }

        public async void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            try
            {
                var id = HttpUtility.UrlDecode(query["CategoryId"]);
                HandlerStates(_getCategoryUseCase.Get(id));
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }

        }


        #endregion
    }
}
