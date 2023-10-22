using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.UseCase.CatalogueClient;
using PuntoDeventa.IU;
using PuntoDeventa.UI.CatalogueClient.Model;
using PuntoDeventa.UI.CatalogueClient.States;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using Xamarin.Forms;

namespace PuntoDeventa.UI.CategoryProduct
{
    internal class CatalogueDetailPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Fields
        private ObservableCollection<Client> _clientList;
        private string _searchText;
        private SalesRoutes _getSalesRoutes;
        private string _title;
        private readonly IAddClientUseCase _addCLientUseCase;
        private readonly IGetSalesRoutesUseCase _getSalesRoutesUseCase;

        #endregion

        #region Contructor
        public CatalogueDetailPageViewModel()
        {
            InicilizeCommand();

            _addCLientUseCase = DependencyService.Get<IAddClientUseCase>();
            _getSalesRoutesUseCase = DependencyService.Get<IGetSalesRoutesUseCase>();
            ClientList = new ObservableCollection<Client>();
        }

        #endregion

        #region Properties
        public string TitlePage
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        public ObservableCollection<Client> ClientList
        {
            get
            {
                if (_clientList.IsNull())
                {
                    _clientList = new ObservableCollection<Client>();
                }
                return _clientList;
            }
            set => SetProperty(ref _clientList, value);
        }
        public string SearchText
        {
            get => _searchText;
            private set => SetProperty(ref _searchText, value);
        }

        private SalesRoutes GetSalesRoutes
        {
            get => _getSalesRoutes;
            set
            {
                if (value.Clients.Count > 0)
                    ClientList = new ObservableCollection<Client>(value.Clients);

                SetProperty(ref _getSalesRoutes, value);
            }
        }


        #endregion

        #region Command

        public Command<string> SearchBarCommand { get; set; }

        public Command<Client> ClientChangedCommand { get; set; }

        public Command NewClientCommand { get; set; }
        #endregion

        #region Methods

        private void InicilizeCommand()
        {
            SearchBarCommand = new Command<string>((test) =>
            {
                SetProductList(test);
                _searchText = test;
            });

            ClientChangedCommand = new Command<Client>((client) =>
            {
                if (client.IsNotNull())
                {
                    // await Shell.Current.GoToAsync($"{nameof(ClientPage)}?ClientId={client.Id}&RouteId={client.RouteId}", true);
                }
            });

            NewClientCommand = new Command(async () =>
            {

                //await Shell.Current.GoToAsync($"{nameof(ClientPage)}?CategoryId={GetSalesRoutes.Id}", true);

            });
        }

        private async void HandlerStates(CatalogeState state)
        {
            switch (state)
            {
                case CatalogeState.Success success:

                    GetSalesRoutes = (SalesRoutes)success.Data;
                    TitlePage = GetSalesRoutes.Name;
                    break;
                case CatalogeState.Error error:
                    await Shell.Current.DisplayAlert("Error", error.Message, "Ok");
                    break;
            }
        }

        private void SetProductList(string name = null)
        {
            if (name.IsNotNull())
                ClientList = new ObservableCollection<Client>(GetSalesRoutes.Clients?.Where(c => c.Name.ToLower().Contains(name.ToLower())));
            else
                ClientList = new ObservableCollection<Client>(GetSalesRoutes.Clients);
        }

        public async void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            try
            {
                var id = HttpUtility.UrlDecode(query["RouteId"]);
                HandlerStates(_getSalesRoutesUseCase.Get(id));
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }

        }


        #endregion
    }
}
