using PuntoDeventa.Core.LocalData.DataBase;
using PuntoDeventa.Core.LocalData.Preferences;
using PuntoDeventa.Core.Network;
using PuntoDeventa.Data.DTO.CatalogueClient;
using PuntoDeventa.Data.DTO;
using PuntoDeventa.UI.CatalogueClient.Model;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using PuntoDeventa.UI.CatalogueClient.States;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.Models;
using System.IO;
using Xamarin.Forms;
using PuntoDeventa.UI.CatalogueClient.Models;
using System;

namespace PuntoDeventa.Data.Repository.CatalogueClient
{
    internal class CatalogueClienteRepository: BaseRepository
    {
        private IDataStore _dataStore;
        private IDataPreferences _dataPreferences;
        private IDataAccessObject _DAO;
        private string tokenID;
        private IElectronicEmissionSystem _emisionSystem;

        public CatalogueClienteRepository()
        {
            _dataStore = DependencyService.Get<IDataStore>();
            _dataPreferences = DependencyService.Get<IDataPreferences>();
            _DAO = DependencyService.Get<IDataAccessObject>();
            tokenID = _dataPreferences.GetUserData().IdToken;
            _emisionSystem = DependencyService.Get<IElectronicEmissionSystem>();
        }

        public async Task<CatalogeState> InsertRoute(SalesRoutes item)
        {
            var resultType = await MakeCallNetwork<SalesRoutesDTO>(() =>
            {
                var dto = new SalesRoutesDTO();
                dto.CopyPropertiesFrom(item);
                return _dataStore.PostAsync(dto, GetUri($"CatalogueClient/SalesRoutes"));
            });

            return ResultTypeToCatalogeState(OperationDTO.InsertOrUpdate, item, resultType);
        }

        public async Task<CatalogeState> InsertClient(Client item)
        {
            var resultType = await MakeCallNetwork<ClientDTO>(() =>
            {
                var dto = new ClientDTO();
                dto.CopyPropertiesFrom(item);
                return _dataStore.PostAsync(dto, GetUri($"CatalogueClient/Clients"));
            });

            return ResultTypeToCatalogeState(OperationDTO.InsertOrUpdate, item, resultType);
        }

        public async Task<CatalogeState> GetTributaryInformation(Rut rut)
        {
            var resultType = await MakeCallNetwork<ClientDTO>(() =>
            {
                //TODO falta cargar apikey desde EcommerceData
                return _emisionSystem.GetAsync("928e15a2d14d4a6292345f04960f4bd3", new Uri(Path.Combine(Properties.Resources.BaseUrlEelectrinicEmision, $"taxpayer", rut.NumberDv)));
            });

            return ResultTypeToCatalogeState(OperationDTO.InsertOrUpdate, new Client(), resultType);
        }


        public async IAsyncEnumerable<SalesRoutes> GetCatalogueAsync()
        {
            var routeResultTask = _dataStore.GetAsync<SalesRoutesDTO>(GetUri("CatalogueClient/SalesRoutes"));

            var clientResultTask = _dataStore.GetAsync<ClientDTO>(GetUri("CatalogueClient/Clients"));

            await Task.Run(async () => { 
                
                await Task.WhenAll(routeResultTask, clientResultTask);

            });

            var routeDeferred = await MakeCallNetwork<Dictionary<string, SalesRoutesDTO>>(routeResultTask.Result);

            var clientDeferred = await MakeCallNetwork<Dictionary<string, ClientDTO>>(clientResultTask.Result);

            var groupedClient = clientDeferred.Data?.Select(c =>
            {
                c.Value.Id = c.Key;
                return c.Value;
            }).ToList().GroupBy(g => g.RouteId);

            var clientDictionary = groupedClient.ToDictionary(group => group.Key, group => group.ToList());

            var routeList = routeDeferred.Data?.Select(c =>
            {
                c.Value.Id = c.Key;
                return c.Value;
            }).ToList();

            foreach (var data in routeList)
            {
                data.Clients = clientDictionary.TryGetValue(data.Id, out var clients) ? clients : new List<ClientDTO>();

                var route = new SalesRoutes();

                route.CopyPropertiesFrom(data);

                yield return route;
            }



        }

        private Uri GetUri(string path)
        {
            return new Uri(Path.Combine(Properties.Resources.BaseUrlRealDataBase, $"{path}.json?auth={tokenID}"));
        }

        private CatalogeState ResultTypeToCatalogeState<T>(OperationDTO method, object item, ResultType<T> resultType)
        {
            if (resultType.Success)
            {
                item.CopyPropertiesFrom(resultType.Data);
                switch (method)
                {
                    case OperationDTO.Delete:
                        //_DAO.Delete(item);
                        break;
                    default:
                        // _DAO.InsertOrUpdate(item);
                        break;
                }
                return new CatalogeState.Success(item);
            }
            else
            {
                return new CatalogeState.Error(string.Join(Environment.NewLine, resultType.Errors));
            }
        }

    }
}
