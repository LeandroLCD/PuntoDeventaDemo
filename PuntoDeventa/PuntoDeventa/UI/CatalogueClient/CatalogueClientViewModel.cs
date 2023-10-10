using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.UseCase.CatalogueClient;
using PuntoDeventa.Domain.UseCase.CatalogueClient.Implementation;
using PuntoDeventa.IU;
using PuntoDeventa.UI.CatalogueClient.Model;
using PuntoDeventa.UI.CatalogueClient.States;
using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.CategoryProduct.States;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PuntoDeventa.UI.CatalogueClient
{
    internal class CatalogueClientViewModel : BaseViewModel
    {
        #region Fields
        private ObservableCollection<SalesRoutes> _salesRoutesList;
        private string _searchText;
        private bool _isVisibleAddCategory;
        private SalesRoutes _newSalesRoutes;
        private readonly IGetRoutesUseCase _getRoutesUseCase;
        private readonly IAddSalesRouteUseCase _addRouteUseCase;
        private readonly IDeleteRouteUseCase _deleteRouteUserCase;

        #endregion

        #region Contructor
        public CatalogueClientViewModel()
        {
            InicilizeCommand();

           _getRoutesUseCase = DependencyService.Get<IGetRoutesUseCase>();

            _addRouteUseCase = DependencyService.Get<IAddSalesRouteUseCase>();

            _deleteRouteUserCase = DependencyService.Get<IDeleteRouteUseCase>();

        }

        #endregion

        #region Properties
        public ObservableCollection<SalesRoutes> SalesRoutesList
        {
            get
            {
                if (GetRouteList.IsNull())
                {
                    _salesRoutesList = new ObservableCollection<SalesRoutes>();
                }
                return _salesRoutesList;
            }
        }
        public string SearchText
        {
            get => _searchText;
            private set => SetProperty(ref _searchText, value);
        }

        public bool IsVisibleAddSalesRoute
        {
            get => _isVisibleAddCategory;
            private set => SetProperty(ref _isVisibleAddCategory, value);
        }

        public SalesRoutes NewSalesRoute
        {
            get
            {

                if (_newSalesRoutes.IsNull())
                {
                    _newSalesRoutes = new SalesRoutes();
                }
                return _newSalesRoutes;
            }
            private set => SetProperty(ref _newSalesRoutes, value);
        }

        private LinkedList<SalesRoutes> GetRouteList { set; get; } = new LinkedList<SalesRoutes>();

        private CancellationTokenSource TokenSource { set; get; }

        #endregion

        #region Command
        public Command IsVisibleAddSalesRoutesCommand { get; set; }

        public Command<string> SearchBarCommand { get; set; }

        public Command<SalesRoutes> NewSalesRoutesCommand { get; set; }

        public Command<SalesRoutes> SalesRoutesChangedCommand { get; set; }

        public Command<SalesRoutes> DeleteSalesRoutesCommand { get; set; }

        public Command<string> RouteChangedCommand {get; set;}

        #endregion

        #region Methods

        public void OnStar()
        {
            InicializeProperties();
        }

        public void OnStop()
        {
            TokenSource.Cancel();
            IsVisibleAddSalesRoute = false;
        }
        private void InicializeProperties()
        {
            TokenSource = new CancellationTokenSource();

            Task.Run(async () =>
            {

                await foreach (var list in _getRoutesUseCase.Emit(TokenSource.Token, 500))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        GetRouteList = new LinkedList<SalesRoutes>(list);
                        SetRoutesList(SearchText);
                    });

                }


            }, TokenSource.Token);

            _salesRoutesList = new ObservableCollection<SalesRoutes>();
        }

        private void InicilizeCommand()
        {
            IsVisibleAddSalesRoutesCommand = new Command(() =>
            {
                IsVisibleAddSalesRoute = !IsVisibleAddSalesRoute;
            });
            SearchBarCommand = new Command<string>((text) =>
            {
                SetRoutesList(text);
                _searchText = text;
            });

            NewSalesRoutesCommand = new Command<SalesRoutes>(async (route) =>
            {
                IsVisibleAddSalesRoutesCommand.Execute(null);
                HandlerStates(await _addRouteUseCase.Insert(route));
            });

            SalesRoutesChangedCommand = new Command<SalesRoutes>(async (route) =>
            {
                if (route.IsNotNull())
                {
                    var state = Shell.Current.Navigation.NavigationStack;
                   await Shell.Current.GoToAsync($"{nameof(CatalogueDetailPage)}?RouteId={route.Id}");
                }

            });

            DeleteSalesRoutesCommand = new Command<SalesRoutes>(async (routes) =>
            {
                if (routes.IsNotNull() && routes.Clients.Count.Equals(0))
                {
                    
                    if (await Shell.Current.DisplayAlert("Advertencia", $"Estas seguro que deseas eliminar la la ruta {routes.Name}.", "Aceptar", "Cancelar"))
                    {
                        var state = await _deleteRouteUserCase.DeleteRoute(routes);
                        if (state is CatalogeState.Error error)
                        {
                            await Shell.Current.DisplayAlert("Error", error.Message, "Ok");
                        }
                    }

                }
                else if (routes.Clients.Count > 0)
                {
                    await Shell.Current.DisplayAlert("Notificacion", "No se puede eliminar una ruta si contiene clientes.", "Ok");
                }

            });

            RouteChangedCommand = new Command<string>((value) => {
                NewSalesRoute.Name = value;
            });

        }

        private async void HandlerStates(CatalogeState categoryStates)
        {
            switch (categoryStates)
            {
                case CatalogeState.Success success:
                    NewSalesRoute = new SalesRoutes();
                    IsVisibleAddSalesRoute = false;
                    await Shell.Current.DisplayAlert("Punto de Venta", $"Ruta {((SalesRoutes)success.Data).Name} creada!!!", "Ok"); ;
                    break;
                case CatalogeState.Error error:
                    await Shell.Current.DisplayAlert("Error", error.Message, "Ok");
                    break;
            }
        }

        private void SetRoutesList(string name = null)
        {
            if (name.IsNotNull())
                _salesRoutesList = new ObservableCollection<SalesRoutes>((GetRouteList.Where(c =>
                c.Name.ToLower().Contains(name.ToLower()))?.ToList()));
            else
                _salesRoutesList = new ObservableCollection<SalesRoutes>(GetRouteList);
            NotifyPropertyChanged(nameof(SalesRoutesList));
        }




        #endregion
    }
}
