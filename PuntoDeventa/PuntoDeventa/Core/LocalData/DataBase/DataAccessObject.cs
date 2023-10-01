using PuntoDeventa.Core.LocalData.DataBase.Entities.CatalogueClient;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using Xamarin.Essentials;

namespace PuntoDeventa.Core.LocalData.DataBase
{
    internal class DataAccessObject : IDisposable,  IDataAccessObject
    {
        private readonly SQLiteConnection _connection;

        public DataAccessObject()
        {
            _connection = new SQLiteConnection(Path.Combine(FileSystem.AppDataDirectory, "DataBase.db3"));
            CreateTables();
        }

       

        public void Delete<T>(T moldel)
        {
            _connection.Delete(moldel);
        }

        public void Delete<T>(IEnumerable<object> moldelList)
        {
            using (var transactionScope = new TransactionScope())
            {
                moldelList.AsParallel().ForAll(item =>
                {
                    _connection.BeginTransaction();

                    _connection.Delete(item, true);

                    _connection.Commit();

                });

                transactionScope.Complete();
            }
        }

        

        public T Get<T>(object primaryKey) where T : new()
        {
            return _connection.GetWithChildren<T>(primaryKey);
        }

        public IEnumerable<T> GetAll<T>() where T : new()
        {
            return _connection.GetAllWithChildren<T>();
        }

        public void InsertOrUpdate<T>(T moldel)
        {
             _connection.InsertOrReplaceWithChildren(moldel, true);
        }

        public void InsertOrUpdate<T>(IEnumerable<T> moldelList)
        {
            _connection.InsertOrReplaceAllWithChildren(moldelList);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private void CreateTables()
        {            
            _connection.CreateTable<CategoryEntity>();
            _connection.CreateTable<ProductEntity>();

            _connection.CreateTable<SalesRoutesEntity>();
            _connection.CreateTable<ClientEntity>();
            _connection.CreateTable<EconomicActivitiesEntity>();
            _connection.CreateTable<BranchOfficesEntity>();
        }
    }
}
