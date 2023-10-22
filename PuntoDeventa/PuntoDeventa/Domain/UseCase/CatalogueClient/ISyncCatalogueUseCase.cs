namespace PuntoDeventa.Domain.UseCase.CatalogueClient
{
    public interface ISyncCatalogueUseCase
    {
        /// <summary>
        /// Retorna el catalogo ordenado en las rutas, desde firebase, y actualiza la información SQLite, en un flow data.
        /// </summary>
        /// <returns></returns>
        void Sync();
    }
}
