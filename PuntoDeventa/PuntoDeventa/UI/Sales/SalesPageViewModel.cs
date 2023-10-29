using PuntoDeventa.Data.Repository.EmissionSystem;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.UseCase.CatalogueClient;
using PuntoDeventa.Domain.UseCase.CategoryProduct;
using PuntoDeventa.IU;
using PuntoDeventa.UI.CatalogueClient.Model;
using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.Controls;
using PuntoDeventa.UI.Sales.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

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
        private bool _isVisibleKeyboard;
        private double _totalSale;
        private double _totalNeto;

        #endregion

        #region Constructor
        public SalesPageViewModel()
        {
            _routesUseCase = DependencyService.Get<IGetRoutesUseCase>();
            _getCategoriesUseCase = DependencyService.Get<IGetCategoryListUseCase>();

            InitializeProperties();
        }
        #endregion

        #region Properties

        private Sale NewSale { get; set; }

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

        public bool IsVisibleKeyboard
        {
            get => _isVisibleKeyboard;
            set => SetProperty(ref _isVisibleKeyboard, value);
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

        public double TotalSale
        {
            get => _totalSale;
            private set => SetProperty(ref _totalSale, value);
        }

        public double TotalNeto
        {
            get => _totalNeto;
            private set => SetProperty(ref _totalNeto, value);
        }

        public SalesRoutes SalesRoutesSelect
        {
            get => _salesRoutesSelect;
            private set => SetProperty(ref _salesRoutesSelect, value);
        }

        private LinkedList<SalesRoutes> GetSalesRoutes { get; set; }

        public ObservableCollection<SalesRoutes> SalesRoutesList
        {
            get
            {
                if (_salesRoutesList.IsNull())
                {
                    _salesRoutesList = new ObservableCollection<SalesRoutes>();
                }
                return _salesRoutesList;
            }
            private set => SetProperty(ref _salesRoutesList, value);
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

        public int RoutesIndex
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
                if (_brands.IsNull())
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
                if (_categoryProductList.IsNull())
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
                if (_getProducts.IsNull())
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
            set => SetProperty(ref _productSales, value);
        }

        private List<ProductSales> GetProductSales { get; set; }
        private CancellationTokenSource TokenSource { get; set; }
        #endregion

        #region Command
        public Command InsertSale { get; set; }
        public Command<StandardEntry> BarCodeChangedCommand { get; set; }
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

        public Command<Entry> QuantityChangedCommand { get; set; }

        public Command<CollectionView> ProductChangedCommand { get; set; }

        public Command<ProductSales> DeleteProductCommand { get; set; }


        #endregion

        #region Methods

        private void InitializeProperties()
        {

            RoutesIndex = -1;
            DateDte = DateTime.Now;
            ClientName = "Seleccione un Cliente";

            GetSalesRoutes = new LinkedList<SalesRoutes>();
            GetCategories = new LinkedList<Category>();
            GetProductSales = new List<ProductSales>();

            InitializeCommand();
        }
        private void InitializeCommand()
        {

            IsVisibleModalRoutesCommand = new Command(() =>
            {

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

            SalesRoutesSelectCommand = new Command<SalesRoutes>((route) =>
            {
                SalesRoutesSelect = route;
                GetClients = new ObservableCollection<Client>(route.Clients);

            });

            ClientSelectCommand = new Command<Client>((client) =>
            {
                client?.Apply(() =>
                {

                    ClientSelect = client;
                    ClientName = client?.Name;
                });
                IsVisibleModalRoutes = false;
            });

            DateDteCommand = new Command<DatePicker>((dateDte) =>
            {

                dateDte.Focus();
            });

            SelectBrandCommand = new Command<string>((bran) =>
            {
                if (bran.Contains("Todo"))
                {
                    GetProducts = new ObservableCollection<Product>(GetCategories.SelectMany(p => p.Products));
                }
                else
                {
                    CategoryProductList = GetCategories.Where(c => c.Brand.Contains(bran)).ToObservableCollection();
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
                    if (ProductCount + 1 <= 22)
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
                CalculateSale();
            });

            QuantityChangedCommand = new Command<Entry>((quantity) =>
            {
                if (quantity.IsNull()) return;

                var product = (ProductSales)quantity.BindingContext;

                var q = string.IsNullOrEmpty(quantity.Text) ? 0 : int.Parse(quantity.Text);

                var productTarget = GetProductSales.FirstOrDefault(p => p.Id == product.Id);

                productTarget?.Apply(() =>
                {
                    if (productTarget.Equals(product)) return;

                    productTarget.Quantity = q;
                    var index = _productSales.IndexOf(product);
                    //ProductsSales.RemoveAt(index);
                    //ProductsSales.Insert(index, productTarget.Clone<ProductSales>());  
                    ProductsSales[index] = product.Clone<ProductSales>();

                    NotifyPropertyChanged(nameof(ProductsSales));

                });
                CalculateSale();
            });

            EditProductCommand = new Command<ProductSales>((product) =>
            {
                if (!product.IsNotNull()) return;
                var productSales = GetProductSales.FirstOrDefault(p => p.Id == product.Id);
                productSales?.Apply(() =>
                {
                    if (productSales.Equals(product)) return;
                    productSales.Quantity = product.Quantity;
                    productSales.IsOffer = product.IsOffer;
                    var index = _productSales.IndexOf(product);
                    //ProductsSales.RemoveAt(index);
                    //ProductsSales.Insert(index, productSales.Clone<ProductSales>());  
                    ProductsSales[index] = product.Clone<ProductSales>();
                    NotifyPropertyChanged(nameof(ProductsSales));

                });
                CalculateSale();

            });

            BarCodeChangedCommand = new Command<StandardEntry>((barCode) =>
            {
                barCode?.Apply(() =>
                {
                    var length = barCode.Text.Length;

                    if ((length <= 12 && !barCode.IsVisibleKeyboard) ||
                        (length < 6 && barCode.IsVisibleKeyboard)) return;

                    var productList = GetCategories.SelectMany(c => c.Products);
                    var product = productList.FirstOrDefault(p =>
                        p.BarCode.ToString().Contains(barCode.Text) ||
                        p.SkuCode.Contains(barCode.Text.ToUpper()) ||
                        p.Name.ToUpper().Contains(barCode.Text.ToUpper()));
                    if (product.IsNull())
                        return;
                    AddProduct(product);
                    barCode.Text = string.Empty;



                });
            });

            InsertSale = new Command(InsertMethods);

        }

        private async void InsertMethods(object obj)
        {
            var branch = ClientSelect.BranchOffices[0];
            var acti = ClientSelect.EconomicActivities[0];
            NewSale = new Sale()
            {
                Client = ClientSelect,
                SelectBranchOffices = branch,
                SelectEconomicActivities = acti ,
                DateSale = DateDte,
                Products = GetProductSales
            };

            var repository = new OpenFacturaRepository();

             var state = await repository.ToEmitDte(new PaymentSales()
            {
                Sale = NewSale,
                DocumentType = DocumentType.Factura,
                PaymentMethod = PaymentMethod.Credit

            });
            Console.WriteLine(state);
            //TODO Construir useCase
            /*
             _caseUse.Inset(NewSale, async () =>
            {
                var json = JsonConvert.SerializeObject(NewSale);
                await Shell.Current.GoToAsync($"{nameof(PaymentSale)}?Sale={json}");
            });
            */
        }

        private void AddProducts(CollectionView view)
        {
            var products = view.SelectedItems.OfType<Product>().ToList();
            products?.ForEach(p =>
            {
                AddProduct(p);

                view.SelectedItems.Remove(p);
            });

            IsVisibleProductCommand.Execute(null);
        }

        private void AddProduct(Product productSource)
        {
            var product = GetProductSales.FirstOrDefault(s => s.Id == productSource.Id);
            if (product.IsNotNull())
            {

                Debug.Assert(product != null, nameof(product) + " != null");

                product.Quantity += 1;

                var productTarget = ProductsSales.FirstOrDefault(s => s.Id == productSource.Id);

                Debug.Assert(productTarget != null, nameof(productTarget) + " != null");

                var index = _productSales.IndexOf(productTarget);

                productTarget.Quantity += 1;
                //Ojito relativamente super importante!!!
                _productSales[index] = productTarget.Clone<ProductSales>();

                NotifyPropertyChanged(nameof(ProductsSales));

            }
            else
            {
                GetProductSales.Add(new ProductSales(productSource));
                ProductsSales.Add(new ProductSales(productSource));
            }
            CalculateSale();
        }

        private void CalculateSale()
        {
            //TODO Consumir iva desde FireBase
            var iva = 0.19;
            var neto = Math.Floor(ProductsSales.Sum(p => p.SubTotal));
            TotalSale = neto * (1 + iva);
            TotalNeto = neto;
        }

        public void OnStart()
        {
            TokenSource = new CancellationTokenSource();

            var taskCategories = Task.Run(async () =>
            {
                await foreach (var categories in _getCategoriesUseCase.Emit(TokenSource.Token, 2000))
                {
                    if (GetCategories.SequenceEqual(categories)) continue;
                    var brands = new LinkedList<string>(categories.Select(c => c.Brand).Distinct());
                    brands.AddFirst("Todo");

                    Brands = new ObservableCollection<string>(brands);
                    GetCategories = new LinkedList<Category>(categories);
                    if (CategorySelect.IsNull())
                    {
                        GetProducts = new ObservableCollection<Product>(categories.SelectMany(p => p.Products));
                    }
                    else
                    {
                        CategorySelect = categories.FirstOrDefault(c => c.Id == CategorySelect.Id);
                        CategorySelect?.Apply(() =>
                        {
                            GetProducts = new ObservableCollection<Product>(CategorySelect.Products);

                        });

                    }
                }
            }, TokenSource.Token);

            var taskRoutes = Task.Run(async () =>
            {
                await foreach (var routes in _routesUseCase.Emit(TokenSource.Token, 2000))
                {
                    if (GetSalesRoutes.SequenceEqual(routes)) continue;
                    GetSalesRoutes = new LinkedList<SalesRoutes>(routes);
                    if (SalesRoutesSelect.IsNull())
                    {
                        GetClients = new ObservableCollection<Client>(routes.SelectMany(route => route.Clients).ToList());
                    }
                    else
                    {
                        SalesRoutesSelect = GetSalesRoutes.FirstOrDefault(r => r.Id.Equals(SalesRoutesSelect.Id));
                        SalesRoutesSelect?.Apply(() =>
                        {
                            GetClients = new ObservableCollection<Client>(SalesRoutesSelect.Clients);

                        });
                    }
                }
            }, TokenSource.Token);

            Task.WhenAll(taskRoutes, taskCategories);
        }

        public void OnStop()
        {
            TokenSource?.Cancel();
        }

        #endregion

    }
}
