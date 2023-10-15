using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.UseCase.CatalogueClient;
using PuntoDeventa.Domain.UseCase.CategoryProduct;
using PuntoDeventa.IU;
using PuntoDeventa.UI.CatalogueClient.Model;
using PuntoDeventa.UI.CategoryProduct.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PuntoDeventa.UI.Sales
{
    internal class SalesPageViewModel : BaseViewModel
    {
        #region Fields
        private readonly IGetRoutesUseCase _routesUseCase;
        private readonly IGetCategoryListUseCase _getCategoriesUseCase;
        private Client _clientSelect;
        private ObservableCollection<SalesRoutes> _salesRoutesList;
        private SalesRoutes _salesRoutesSelect;
        private ObservableCollection<Client> _clientList;
        private bool _isVisibleModelClient;
        private string _clientName;
        private int _index;
        private bool _isVisibleTributaryData;
        private DateTime _dateDte;
        private bool _isVisibleProduct;
        private ObservableCollection<string> _brands;
        private ObservableCollection<Category> _categoryProductList;
        private Category _categorySelect;
        private ObservableCollection<Client> _getClients;
        private ObservableCollection<Product> _getProducts;
        private int _productCount;
        private ObservableCollection<ProductSales> _productSales;

        #endregion

        #region Constructor
        public SalesPageViewModel()
        {
           _routesUseCase = DependencyService.Get<IGetRoutesUseCase>();
           _getCategoriesUseCase = DependencyService.Get<IGetCategoryListUseCase>();
            RoutesIndes = -1;
            DateDte = DateTime.Now;

            InicializeCommand();
        }
        #endregion

        #region Properties

        public bool IsVisibleTributaryData
        {
            get => _isVisibleTributaryData;
            private set => SetProperty(ref _isVisibleTributaryData, value);
        }

        public bool IsVisibleProduct
        {
            get => _isVisibleProduct;
            private set => SetProperty(ref _isVisibleProduct, value);
        }
        public string ClientName
        {
            get => _clientName;
            private set => SetProperty(ref _clientName, value);     
        }
        public Client ClientSelect
        { 
            get => _clientSelect;
            set => SetProperty(ref _clientSelect, value);        
        }

        public SalesRoutes SalesRoutesSelect
        {
            get => _salesRoutesSelect;
            private set
            {
                SetProperty(ref _salesRoutesSelect, value);
            } 
        }

        private LinkedList<SalesRoutes> GetSalesRoutes { get; set; }

        public ObservableCollection<SalesRoutes> SalesRoutesList
        {
            get 
            {
                if(_salesRoutesList.IsNull())
                {
                    _salesRoutesList = new ObservableCollection<SalesRoutes>();
                }
                return _salesRoutesList;
            }
            private set
            {

                SetProperty(ref _salesRoutesList, value);
            }
        }

        public ObservableCollection<Client> GetClients
        {
            get
            {
                if (_clientList.IsNull())
                {
                    _clientList = new ObservableCollection<Client>();
                }
                return _clientList;
            }
            private set => SetProperty(ref _clientList, value);
        }

        public bool IsVisibleModalRoutes
        {
            get => _isVisibleModelClient;
            private set => SetProperty(ref _isVisibleModelClient, value);
        }
        public int RoutesIndes
        {
            get => _index;
            set => SetProperty(ref _index, value);
        }

        public DateTime DateDte
        {
            get => _dateDte;
            set => SetProperty(ref _dateDte, value);
        }

        private LinkedList<Category> GetCategories { get; set; }
        public ObservableCollection<string> Brands
        {
            get 
            {
                if(_brands.IsNull())
                {
                    _brands = new ObservableCollection<string>();
                }
                return _brands;
            }
            private set => SetProperty(ref _brands, value);
        }

        public ObservableCollection<Category> CategoryProductList
        {
            get
            {
                if(_categoryProductList.IsNull())
                {
                    _categoryProductList = new ObservableCollection<Category>();
                }
                return _categoryProductList;
            }
            private set => SetProperty(ref _categoryProductList, value);
        }

        public ObservableCollection<Product> GetProducts
        {
            get
            {
                if(_getProducts.IsNull())
                {
                    _getProducts = new ObservableCollection<Product>();
                }
                return _getProducts;
            }
            private set => SetProperty(ref _getProducts, value);
        }
        public int ProductCount
        { 
            get => _productCount; 
            private set => SetProperty(ref _productCount, value);
        }
        public Category CategorySelect
        {
            get => _categorySelect; 
            private set => SetProperty(ref _categorySelect, value);
        }
        
        public ObservableCollection<ProductSales> ProductsSales
        {
            get
            {
                if (_productSales.IsNull())
                {
                    _productSales = new ObservableCollection<ProductSales>();
                }
                return _productSales;
            }
            private set => SetProperty(ref _productSales, value);
        }

        private List<ProductSales> GetProductSales { get; set; }
        private CancellationTokenSource TokenSource { get; set; }
        #endregion

        #region Command

        public Command<string> SelectBrandCommand { get; set; }
        public Command<Client> ClientSelectCommand { get; set; }

        public Command<Category> CategorySelectCommand { get; set; }

        public Command<SalesRoutes> SalesRoutesSelectCommand { get; set; }


        public Command IsVisibleModalRoutesCommand { get; set; }

        public Command IsVisibleTributaryDataCommand { get; set; }

        public Command IsVisibleProductCommand { get; set; }

        public Command<DatePicker> DateDteCommand { get; set; }

        public Command<CollectionView> AddProductsCommand { get; set; }

        public Command<ProductSales> EditProductCommand { get; set; }

        public Command<CollectionView> ProductChangedCommand { get; set; }

        public Command<ProductSales> DeleteProductCommand { get; set; }


        #endregion

        #region Methods

        private void InicializeCommand()
        {
            ClientName = "Seleccione un Cliente";
            IsVisibleModalRoutesCommand = new Command(() => {

                IsVisibleModalRoutes = !IsVisibleModalRoutes;
            });

            IsVisibleTributaryDataCommand = new Command(() =>
            {
                IsVisibleTributaryData = !IsVisibleTributaryData;
            });

            IsVisibleProductCommand = new Command(() =>
            {
                IsVisibleProduct = !IsVisibleProduct;
            });

            SalesRoutesSelectCommand = new Command<SalesRoutes>((route)=>{ 
                SalesRoutesSelect = route;
                GetClients = new ObservableCollection<Client>(route.Clients);
                
            });

            ClientSelectCommand = new Command<Client>((client) => {
                client?.Apply(() => {

                    ClientSelect = client;
                    ClientName = client?.Name;
                });
                IsVisibleModalRoutes = false;
            });

            DateDteCommand = new Command<DatePicker>((dateDte) => {

                dateDte.Focus();
            });

            SelectBrandCommand = new Command<string>((bran) => {
                if (bran.Contains("Todo"))
                {
                    GetProducts = new ObservableCollection<Product>(GetCategories.SelectMany(p => p.Products));
                }
                else
                {
                    CategoryProductList = GetCategories.Where( c => c.Brand.Contains(bran) ).ToObservableCollection();
                    GetProducts = new ObservableCollection<Product>(CategoryProductList.SelectMany(c => c.Products));
              
                }
                
            });

            CategorySelectCommand = new Command<Category>((category) =>
            {
                GetProducts = new ObservableCollection<Product>(category.Products);
            });

            ProductChangedCommand = new Command<CollectionView>((collection) =>
            {

                collection?.Apply(() =>
                {
                    if(ProductCount + 1 <= 22)
                    { 
                        ProductCount = ProductsSales.Count() + collection.SelectedItems.Count();
                   
                    }
                    else
                    {
                        //var ultProd = collection.SelectedItems.Last();
                        //ultProd?.Apply(() =>
                        //{
                        //    collection.SelectedItems.Remove(ultProd);
                        //    ProductCount = ProductsSales.Count() + collection.SelectedItems.Count();
                        //});
                        
                    }
                });
            });

            AddProductsCommand = new Command<CollectionView>(AddProducts);

            DeleteProductCommand = new Command<ProductSales>((products) =>
            {
                ProductsSales.Remove(products);
                GetProductSales.Remove(products);
            });

            EditProductCommand = new Command<ProductSales>((product) => {
                if (product.IsNotNull())
                {
                    var productTarge = GetProductSales.FirstOrDefault(p => p.Id == product.Id);
                    productTarge?.Apply(() =>
                    {
                       
                        if(productTarge != product)
                        {
                            NotifyPropertyChanged(nameof(ProductsSales));
                        }
                        
                    });
                }

            });
        }

        private void AddProducts(CollectionView view)
        {
            var produts = view.SelectedItems.OfType<Product>().ToList();
            produts?.ForEach(p => {
                var product = GetProductSales.FirstOrDefault(s=> s .Id == p.Id);
                if (product.IsNotNull())
                {
                    product.Quantity += 1;
                    var pc = ProductsSales.FirstOrDefault(s => s.Id == p.Id);
                    pc.Quantity += 1;
                }
                else
                {
                    GetProductSales.Add(new ProductSales(p));
                    ProductsSales.Add(new ProductSales(p));
                }
                               
                view.SelectedItems.Remove(p);
            });
           // ProductsSales = new ObservableCollection<ProductSales>(GetProductSales.Clone());
            IsVisibleProductCommand.Execute(null);
        }

        public void OnStar()
        { 
         TokenSource = new CancellationTokenSource();
            GetSalesRoutes = new LinkedList<SalesRoutes>();
            GetCategories = new LinkedList<Category>();
            GetProductSales = new List<ProductSales>();
            //Todo sacar for y unificar los dos Task.Run
            _ = Task.Run(async () =>
            {

                await foreach (var routes in _routesUseCase.Emit(TokenSource.Token, 2000))
                {
                    routes.ForEach(route =>
                     {

                         //var obb = GetSalesRoutes.SequenceEqual(routes);

                         //routes.SelectMany(route => route.Clients).ToList();

                         var obj = GetSalesRoutes.FirstOrDefault(r => r.Name.Equals(route.Name));
                         if (obj.IsNotNull())
                         {
                             obj = route;
                         }
                         else
                         {
                             GetSalesRoutes.AddLast(route);
                         }
                         SalesRoutesList = new ObservableCollection<SalesRoutes>(GetSalesRoutes);



                         if (SalesRoutesSelect.IsNull())
                         {
                             SetClients(route.Clients);
                         }
                         else if (SalesRoutesSelect.Equals(route))
                         {
                             SalesRoutesSelect = route;
                             SetClients(route.Clients);
                         }
                     });


                }


            }, TokenSource.Token);

            _ = Task.Run(async () =>
             {
                 await foreach (var categories in _getCategoriesUseCase.Emit(TokenSource.Token, 2000))
                 {

                     var equal = GetCategories.SequenceEqual(categories);
                     if (!GetCategories.SequenceEqual(categories))
                     {
                         LinkedList<string> brans = new LinkedList<string>(categories.Select(c=> c.Brand).Distinct());
                         brans.AddFirst("Todo");
                         
                         Brands = new ObservableCollection<string>(brans);
                         GetCategories = new LinkedList<Category>(categories);
                         if (CategorySelect.IsNull())
                         {
                             GetProducts = new ObservableCollection<Product>(categories.SelectMany(p => p.Products));
                         }
                         else
                         {
                             CategorySelect = categories.FirstOrDefault(c => c.Id == CategorySelect.Id);
                             GetProducts = new ObservableCollection<Product>(CategorySelect.Products);
                         }
                     }
                 }



                 
             }, TokenSource.Token);

        }

        

        public void OnStop()
        {
            TokenSource?.Cancel();
        }

        private void SetClients(List<Client> clients)
        {
            foreach (Client c in clients)
            {
                if (GetClients.Contains(c))
                {
                    int index = GetClients.IndexOf(c);
                    GetClients[index] = c;
                }
                else
                {
                    GetClients.Add(c);
                }
            }
        }

        #endregion

    }
}
