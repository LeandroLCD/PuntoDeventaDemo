namespace PuntoDeventa.Domain.UseCase.CatalogueClient
{
    using PuntoDeventa.UI.CatalogueClient.Model;
    using System.Collections.Generic;

    public interface ISyncCatalogueUseCase
    {
        /// <summary>
        /// Retorna el catalogo ordenado en las rutas, desde firebase, y actualiza la información SQLite, en un flow data.
        /// </summary>
        /// <returns></returns>
        void Sync();
    }
}
