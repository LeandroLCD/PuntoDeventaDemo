using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.UseCase.CategoryProduct;
using PuntoDeventa.IU;
using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.CategoryProduct.States;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PuntoDeventa.UI.CategoryProduct
{
    internal class CategoryHomeViewModel : BaseViewModel
    {
        #region Fields
        private ObservableCollection<Category> _categoryList;
        private IGetCategoryListUseCase _categoryListUseCase;
        private IAddCategoryUseCase _addCategoryUseCase;
        private string _searchText;
        private bool _isVisibleAddCategory;
        private Category _newCategory;

        #endregion

        #region Contructor
        public CategoryHomeViewModel()
        {
            InicilizeCommand();

            _categoryListUseCase = DependencyService.Get<IGetCategoryListUseCase>();

            _addCategoryUseCase = DependencyService.Get<IAddCategoryUseCase>();

            TokenSource = new CancellationTokenSource();

        }

        #endregion

        #region Properties
        public ObservableCollection<Category> CategoryList
        {
            get
            {
                if (GetCategories.IsNull())
                {
                    _categoryList = new ObservableCollection<Category>();
                }
                return _categoryList;
            }
        }
        public string SearchText
        {
            get => _searchText;
            private set => SetProperty(ref _searchText, value);
        }

        public bool IsVisibleAddCategory
        {
            get => _isVisibleAddCategory;
            private set => SetProperty(ref _isVisibleAddCategory, value);
        }

        public Category NewCategory
        {
            get
            {

                if (_newCategory.IsNull())
                {
                    _newCategory = new Category();
                }
                return _newCategory;
            }
            private set => SetProperty(ref _newCategory, value);
        }

        private LinkedList<Category> GetCategories { set; get; } = new LinkedList<Category>();

        private CancellationTokenSource TokenSource { set; get; }

        #endregion

        #region Command
        public Command IsVisibleAddCategoryCommand { get; set; }

        public Command<string> SearchBarCommand { get; set; }

        public Command<Category> NewCategoryCommand { get; set; }

        public Command<Category> CategoryChangedCommand { get; set; }
        #endregion

        #region Methods

        public void OnApperning()
        {
            InicializeProperties();
        }

        public void OnStop()
        {
            TokenSource.Cancel();
            IsVisibleAddCategory = false;
        }
        private void InicializeProperties()
        {

            Task.Run(async () =>
            {

                await foreach (var list in _categoryListUseCase.Emit(TokenSource.Token))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        GetCategories = new LinkedList<Category>(list);
                        SetCategoryList(SearchText);
                    });

                }


            }, TokenSource.Token);

            _categoryList = new ObservableCollection<Category>();
        }

        private void InicilizeCommand()
        {
            IsVisibleAddCategoryCommand = new Command(() =>
            {
                IsVisibleAddCategory = !IsVisibleAddCategory;
            });
            SearchBarCommand = new Command<string>((text) =>
            {
                SetCategoryList(text);
                _searchText = text;
            });

            NewCategoryCommand = new Command<Category>(async (category) =>
            {
                HandlerStates(await _addCategoryUseCase.Insert(category));
            });

            CategoryChangedCommand = new Command<Category>(async (category) =>
            {
                if (category.IsNotNull())
                {
                    //CategoryHome/CategoryDetailPage?CategoryId=salknsadlnaslish&name=category
                    
                    await Shell.Current.GoToAsync($"{nameof(CategoryDetailPage)}?CategoryId={category.Id}");
                }

            });

        }

        private async void HandlerStates(CategoryStates categoryStates)
        {
            switch (categoryStates)
            {
                case CategoryStates.Success success:
                    IsVisibleAddCategory = false;
                    await Shell.Current.DisplayAlert("Punto de Venta", $"Categoria creada!!!{((Category)success.Data).Name}", "Ok"); ;
                    break;
                case CategoryStates.Error error:
                    await Shell.Current.DisplayAlert("Error", error.Message, "Ok");
                    break;
            }
        }

        private void SetCategoryList(string name = null)
        {
            if (name.IsNotNull())
                _categoryList = new ObservableCollection<Category>(GetCategories.Where(c =>
                c.Name.ToLower().Contains(name.ToLower()))?.ToList());
            else
                _categoryList = new ObservableCollection<Category>(GetCategories);
            NotifyPropertyChanged(nameof(CategoryList));
        }




        #endregion
    }
}
