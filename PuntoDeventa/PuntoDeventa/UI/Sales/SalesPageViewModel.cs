using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.UseCase.CatalogueClient;
using PuntoDeventa.IU;
using PuntoDeventa.UI.CatalogueClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

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

        #endregion

        #region Constructor
        public SalesPageViewModel()
        {
           _routesUseCase = DependencyService.Get<IGetRoutesUseCase>();
            RoutesIndes = -1;
            InicializeCommand();
        }
        #endregion

        #region Properties

        public string ClientName
        {
            get => _clientName;
            private set => SetProperty(ref _clientName, value);     
        }
        private Client ClientSelect
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
        private CancellationTokenSource TokenSource { get; set; }
        #endregion

        #region Command
        public Command<Client> ClientSelectCommand { get; set; }

        public Command<SalesRoutes> SalesRoutesSelectCommand { get; set; }

        public Command IsVisibleModalRoutesCommand { get; set; }
        #endregion

        #region Methods

        private void InicializeCommand()
        {
            ClientName = "Seleccione un Cliente";
            IsVisibleModalRoutesCommand = new Command(() => {

                IsVisibleModalRoutes = !IsVisibleModalRoutes;
            });

            SalesRoutesSelectCommand = new Command<SalesRoutes>((route)=>{ 
                SalesRoutesSelect = route;
            });
        }
        public void OnStar()
        { 
         TokenSource = new CancellationTokenSource();
            Task.Run(async () =>
            {

                await foreach (var list in _routesUseCase.Emit(TokenSource.Token, 1000))
                {
                    //Validar si list es diferente a GetSalesRoutes
                    
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        GetSalesRoutes = new LinkedList<SalesRoutes>(list);
                        if(list.Count != SalesRoutesList.Count)
                        list?.Apply(() => { 
                            SalesRoutesList = new ObservableCollection<SalesRoutes>(list);
                            var routeSelect = SalesRoutesList.IndexOf(SalesRoutesSelect);
                            RoutesIndes = routeSelect;
                        });                            

                        
                        

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
