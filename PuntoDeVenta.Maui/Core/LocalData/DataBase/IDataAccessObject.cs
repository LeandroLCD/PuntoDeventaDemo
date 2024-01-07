namespace PuntoDeVenta.Maui.Core.LocalData.DataBase
{
    public interface IDataAccessObject
    {
        void InsertOrUpdate<T>(T moldel);

        void InsertOrUpdate<T>(IEnumerable<T> moldelList);

        void Delete<T>(T moldel);

        void Delete<T>(IEnumerable<object> moldelList);

        T Get<T>(object primaryKey) where T : new();

        IEnumerable<T> GetAll<T>() where T : new();
    }
}
