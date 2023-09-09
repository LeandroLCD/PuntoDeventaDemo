using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.UseCase.CategoryProduct;
using PuntoDeventa.IU;
using PuntoDeventa.UI.CategoryProduct.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PuntoDeventa.UI.CategoryProduct
{
    internal class CategoryHomeViewModel : BaseViewModel
    {
        private ObservableCollection<Category> _categoryList;
        private IGetCategoryListUseCase _categoryListUseCase;
        #region Fields

        #endregion

        #region Contructor
        public CategoryHomeViewModel()
        {
            InicilizeCommand();

            _categoryListUseCase = DependencyService.Get<IGetCategoryListUseCase>();

            TokenSource = new CancellationTokenSource();

        }

        #endregion

        #region Properties
        public ObservableCollection<Category> CategoryList
        {
            get
            {
                if (GetCategories.IsNotNull())
                {
                    _categoryList = new ObservableCollection<Category>(GetCategories);
                }
                return _categoryList;
            }
        }

        private LinkedList<Category> GetCategories { set; get; }

        private CancellationTokenSource TokenSource { set; get; }

        #endregion

        #region Command

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
        private  void InicializeProperties()
        {

           Task.Run( async () => {

               await foreach (var list in _categoryListUseCase.Emit(TokenSource.Token))
                {

                  Device.BeginInvokeOnMainThread(() =>
                  {
                      GetCategories = new LinkedList<Category>(list);
                      _categoryList = new ObservableCollection<Category>(GetCategories);
                      NotifyPropertyChanged(nameof(CategoryList));
                  });
                    
                }
            
            
            }, TokenSource.Token);  

            _categoryList = new ObservableCollection<Category>();
        }

        private void InicilizeCommand()
        {
        }


        #endregion
    }
}
