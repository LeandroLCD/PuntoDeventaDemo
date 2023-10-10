using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.UseCase.CatalogueClient;
using PuntoDeventa.IU;
using PuntoDeventa.UI.CatalogueClient.Model;
using PuntoDeventa.UI.CategoryProduct.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PuntoDeventa.UI.Sales
{
    internal class SalesPageViewModel : BaseViewModel
    {
        #region Fields
        private readonly IGetRoutesUseCase _routesUseCase;
        private Client _clientSelect;
        private ObservableCollection<SalesRoutes> _salesRoutesList;
        private SalesRoutes _salesRoutesSelect;
        private ObservableCollection<Client> _cleintList;
        private bool _isVisibleModelClient;
        private string _clientName;
        private int _index;
        private bool _isVisibleTributaryData;
        private DateTime _dateDte;
        private bool _isVisibleProduct;
        private ObservableCollection<string> _brands;
        private ObservableCollection<Category> _categoryProductList;
        private Category _categorySelect;

        #endregion

        #region Constructor
        public SalesPageViewModel()
        {
           _routesUseCase = DependencyService.Get<IGetRoutesUseCase>();
            
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
            private set => SetProperty(ref _salesRoutesSelect, value);  
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
            private set => SetProperty(ref _salesRoutesList, value);
        }

        public ObservableCollection<Client> ClientList
        {
            get
            {
                if (_salesRoutesSelect.IsNull() && _salesRoutesSelect.Clients.Count > 0)
                {
                    _cleintList = new ObservableCollection<Client>(_salesRoutesSelect.Clients);
                }
                else
                {
                    _cleintList = new ObservableCollection<Client>();
                }
                return _cleintList;
            }
            private set => SetProperty(ref _cleintList, value);
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

        public ObservableCollection<string> Brands
        {
            get => _brands;
            private set => SetProperty(ref _brands, value);
        }

        public ObservableCollection<Category> CategoryProductList
        {
            get => _categoryProductList;
            private set => SetProperty(ref _categoryProductList, value);
        }

        public Category CategorySelect
        {
            get => _categorySelect; 
            private set => SetProperty(ref _categorySelect, value);
        }

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
            });

            ClientSelectCommand = new Command<Client>((client) => {
                client?.Apply(() => {

                    ClientSelect = client;
                    ClientName = client?.Name;
                });
                IsVisibleModalRoutes = false;
            });

            //Importante
            DateDteCommand = new Command<DatePicker>((dateDte) => {

                dateDte.Focus();
            });
        }
        public void OnStar()
        { 
         TokenSource = new CancellationTokenSource();
            GetSalesRoutes = new LinkedList<SalesRoutes>();
            Task.Run(async () =>
            {

                await foreach (var routes in _routesUseCase.Emit(TokenSource.Token, 2000))
                {
                    routes.ForEach(route => {

                        
                           var obj = GetSalesRoutes.FirstOrDefault(r=> r.Name.Equals(route.Name));
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
                                SalesRoutesSelect = new SalesRoutes()
                                {
                                    Name = "All",
                                    Clients = route.Clients
                                };
                            }
                            else if (SalesRoutesSelect.Name.Contains("All") && route.Clients.Count > 0)
                            {
                                if (!SalesRoutesSelect.Clients.Contains(route.Clients[0]))
                                    SalesRoutesSelect.Clients.AddRange(route.Clients);
                            }
                            if (SalesRoutesSelect.Equals(route))
                            {
                                SalesRoutesSelect = route;
                            }                       

                    });              
                    
                    

                }


            }, TokenSource.Token);



        }

        public void OnStop()
        {
            TokenSource?.Cancel();
        }



        #endregion

    }
}
