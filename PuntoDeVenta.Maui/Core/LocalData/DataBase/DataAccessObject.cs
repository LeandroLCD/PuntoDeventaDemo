

namespace PuntoDeVenta.Maui.Core.LocalData.DataBase
{
    using Microsoft.Data.Sqlite;
    using PuntoDeVenta.Maui.Core.LocalData.DataBase.Entities.CatalogueClient;
    using PuntoDeVenta.Maui.Core.LocalData.DataBase.Entities.CategoryProduct;
    using PuntoDeVenta.Maui.Core.LocalData.DataBase.Entities.Sales;
    using PuntoDeVenta.Maui.Core.LocalData.DataBase.SQL;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Transactions;
    internal class DataAccessObject : IDisposable, IDataAccessObject
    {
        private readonly SqliteConnection _connection;
        public DataAccessObject()
        {
            var path = Path.Combine(FileSystem.CacheDirectory, "DataBase.db3");
            var connectionString = $"Data Source={path};";
            _connection = new SqliteConnection(connectionString);
            _connection.Open();
            CreateTables();
        }

        public void Delete<T>(T moldel)
        {
            _connection.Delete(moldel);
        }

        public void Delete<T>(IEnumerable<object> moldelList)
        {
            using var transactionScope = new TransactionScope();
            var task = _connection.BeginTransaction();
            moldelList.AsParallel().ForAll(item =>
            {

                _connection.Delete(item);

            });
            task.Commit();
            transactionScope.Complete();
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
            _connection.InsertOrReplaceWithChildren(moldel);
        }

        public void InsertOrUpdate<T>(IEnumerable<T> moldelList)
        {
            moldelList.ToList().ForEach( it => 
            _connection.InsertOrReplaceWithChildren(it)
            );
        }
        
        public void Dispose()
        {
            _connection.Close();
        }

       
        private void CreateTables()
        {
            _connection.Map<CategoryEntity>();

            _connection.Map<ProductEntity>();

            _connection.Map<SalesRoutesEntity>();

            _connection.Map<ClientEntity>();

            _connection.Map<EconomicActivitiesEntity>();

            _connection.Map<BranchOfficesEntity>();

            _connection.Map<PendingDocumentEntity>();
        }
    }
}
