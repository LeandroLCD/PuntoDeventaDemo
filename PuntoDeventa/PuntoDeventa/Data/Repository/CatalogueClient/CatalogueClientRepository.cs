﻿namespace PuntoDeventa.Data.Repository.CatalogueClient
{
    using PuntoDeventa.Core.LocalData.DataBase;
    using PuntoDeventa.Core.LocalData.DataBase.Entities.CatalogueClient;
    using PuntoDeventa.Core.LocalData.Preferences;
    using PuntoDeventa.Core.Network;
    using PuntoDeventa.Data.DTO;
    using PuntoDeventa.Data.DTO.CatalogueClient;
    using PuntoDeventa.Data.Mappers;
    using PuntoDeventa.Domain.Helpers;
    using PuntoDeventa.Domain.Models;
    using PuntoDeventa.UI.CatalogueClient.Model;
    using PuntoDeventa.UI.CatalogueClient.Models;
    using PuntoDeventa.UI.CatalogueClient.States;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    internal class CatalogueClientRepository : BaseRepository, ICatalogueClientRepository
    {
        private readonly IDataStore _dataStore;
        private IDataPreferences _dataPreferences;
        private IDataAccessObject _DAO;
        private string tokenID;
        private readonly IElectronicEmissionSystem _emissionSystem;

        public CatalogueClientRepository()
        {
            _dataStore = DependencyService.Get<IDataStore>();
            _dataPreferences = DependencyService.Get<IDataPreferences>();
            _DAO = DependencyService.Get<IDataAccessObject>();
            tokenID = _dataPreferences.GetUserData().IdToken;
            _emissionSystem = DependencyService.Get<IElectronicEmissionSystem>();
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
                return _emissionSystem.GetAsync(new Uri(Path.Combine(Properties.Resources.BaseUrlEelectronicEmission, $"taxpayer", rut.NumberDv)));
            });

            return ResultTypeToCatalogeState(OperationDTO.InsertOrUpdate, new Client(), resultType);
        }

        public List<SalesRoutes> GetRoutesAll()
        {

            return _DAO.GetAll<SalesRoutesEntity>()?.Select(routeEntity => new SalesRoutes()
            {
                Id = routeEntity.Id,
                Name = routeEntity.Name,
                Clients = routeEntity.Clients?.Select(client => client.ToClient()).ToList()
            }).ToList();

        }

        public async IAsyncEnumerable<SalesRoutes> GetCatalogueAsync()
        {
            var routeResultTask = _dataStore.GetAsync<SalesRoutesDTO>(GetUri("CatalogueClient/SalesRoutes"));

            var clientResultTask = _dataStore.GetAsync<ClientDTO>(GetUri("CatalogueClient/Clients"));

            await Task.Run(async () =>
            {

                await Task.WhenAll(routeResultTask, clientResultTask);

            });

            var routeDeferred = await MakeCallNetwork<Dictionary<string, SalesRoutesDTO>>(routeResultTask.Result);

            var clientDeferred = await MakeCallNetwork<Dictionary<string, ClientDTO>>(clientResultTask.Result);


            if (routeDeferred.Success && clientDeferred.Success)
            {
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
                    _DAO.InsertOrUpdate(route.ToSalesRoutesEntity());
                    yield return route;
                }

            }
            else
            {
                var message = $"Se produjo un error al intentar obtener las rutas de los cliente." +
                               $" \n Detalles: {string.Join(Environment.NewLine, routeDeferred.Errors)} {string.Join(Environment.NewLine, clientDeferred.Errors)}";
                await App.Current.MainPage.DisplayAlert("Error", message, "Ok");
            }




        }

        public async Task<bool> Sync()
        {


            var routeResultTask = _dataStore.GetAsync<SalesRoutesDTO>(GetUri("CatalogueClient/SalesRoutes"));

            var clientResultTask = _dataStore.GetAsync<ClientDTO>(GetUri("CatalogueClient/Clients"));

            await Task.Run(async () =>
            {

                await Task.WhenAll(routeResultTask, clientResultTask);

            });

            var routeDeferred = await MakeCallNetwork<Dictionary<string, SalesRoutesDTO>>(routeResultTask.Result);

            var clientDeferred = await MakeCallNetwork<Dictionary<string, ClientDTO>>(clientResultTask.Result);


            if (routeDeferred.Success && clientDeferred.Success)
            {
                var groupedClient = clientDeferred.Data?.Select(c =>
                {
                    c.Value.Id = c.Key;
                    return c.Value;
                }).ToList().GroupBy(g => g.RouteId);

                var clientDictionary = groupedClient?.ToDictionary(group => group.Key, group => group.ToList());

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
                    _DAO.InsertOrUpdate(route.ToSalesRoutesEntity());
                }
                return await Task.FromResult(true);
            }
            else
            {
                var message = $"Se produjo un error al intentar obtener las rutas de los cliente." +
                               $" \n Detalles: {string.Join(Environment.NewLine, routeDeferred.Errors)} {string.Join(Environment.NewLine, clientDeferred.Errors)}";
                await App.Current.MainPage.DisplayAlert("Error", message, "Ok");
                return await Task.FromResult(false);
            }




        }
        public async Task<CatalogeState> DeleteRoute(SalesRoutes item)
        {
            var resultType = await MakeCallNetwork<SalesRoutes>(() =>
            {
                return _dataStore.DeleteAsync<SalesRoutes>(GetUri($"CatalogueClient/SalesRoutes/{item.Id}"));
            });

            return ResultTypeToCatalogeState(OperationDTO.Delete, item, resultType);
        }

        public async Task<CatalogeState> DeleteClient(Client item)
        {
            var resultType = await MakeCallNetwork<Client>(() =>
            {
                return _dataStore.DeleteAsync<Client>(GetUri($"CatalogueClient/Clients/{item.Id}"));
            });

            return ResultTypeToCatalogeState(OperationDTO.Delete, item.ToClientEntity(), resultType);
        }

        public async Task<CatalogeState> UpDateClient(Client item)
        {
            var dto = item.ToClientDTO();
            var resultType = await MakeCallNetwork<ClientDTO>(() =>
            {
                return _dataStore.PutAsync(dto, GetUri($"CatalogueClient/Clients/{item.Id}"));
            });

            var entity = item.ToClientEntity();
            entity.SalesRoutes = _DAO.Get<SalesRoutesEntity>(item.RouteId);


            return ResultTypeToCatalogeState(OperationDTO.InsertOrUpdate, entity, resultType);
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
                var entity = ((SalesRoutes)item).ToSalesRoutesEntity();
                switch (method)
                {
                    case OperationDTO.Delete:
                        _DAO.Delete(entity);
                        break;
                    default:
                        _DAO.InsertOrUpdate(entity);
                        break;
                }
                return new CatalogeState.Success(item);
            }
            else
            {
                return new CatalogeState.Error(string.Join(Environment.NewLine, resultType.Errors));
            }
        }

        public CatalogeState GetSalesRoutes(string id)
        {
            var routeEntity = _DAO.Get<SalesRoutesEntity>(id);
            if (routeEntity.IsNotNull())
            {
                var route = new SalesRoutes()
                {
                    Id = routeEntity.Id,
                    Name = routeEntity.Name,
                    Clients = routeEntity.Clients?.Select(c => c.ToClient()).ToList()
                };
                return new CatalogeState.Success(route);
            }
            return new CatalogeState.Error("Ruta no encontrada");
        }


    }
}
