using PuntoDeventa.Data.Repository.CategoryProduct;
using PuntoDeventa.UI.CategoryProduct.Models;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct.Implementation
{
    internal class GetCategoryListUseCase : IGetCategoryListUseCase
    {
        private ICategoryProductRepository _repository;

        public GetCategoryListUseCase()
        {
            _repository = DependencyService.Get<ICategoryProductRepository>();
        }
        /// <summary>
        /// Retorna un Data Flow, de Categorias con sus productos, requiere un CancellationToken y un tiempo en 
        /// milisegundos que representa el tiempo entre cada llamada.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="inMilisegunds"></param>
        /// <returns></returns>
        /* 
        Descripción:
          Este caso de uso se centra en la obtención de una lista de categorías 
          de productos desde una fuente de datos. Utiliza una interfaz llamada 
          "ICategoryProduct" para acceder a la fuente de datos de las categorías 
          de productos. El caso de uso proporciona una manera asincrónica de obtener 
          estas categorías y se ejecuta en un bucle hasta que se solicite la cancelación 
          mediante un token de cancelación.

          Constructor: El caso de uso tiene un constructor que recibe una instancia de la
          interfaz "ICategoryProduct". Esto permite la inyección de dependencias, lo que facilita 
          la prueba unitaria y la flexibilidad al cambiar la implementación de la fuente de datos en el futuro.

          Método GetCategories: Este método es asincrónico y devuelve un flujo de datos usando 
          "IAsyncEnumerable". Toma dos parámetros: un token de cancelación ("token") y un valor 
          opcional en milisegundos ("inMilisegunds") que especifica el tiempo de retraso entre las 
          llamadas para obtener las categorías.

          Bucle While: Dentro del método "GetCategories", hay un bucle "while" que se ejecutará 
          mientras el token de cancelación no esté solicitando la cancelación ("token.IsCancellationRequested"). 
          Esto significa que el bucle se ejecutará hasta que se solicite explícitamente detenerlo.

          Retraso y Devolución de Datos: Dentro del bucle, hay un retraso usando "Task.Delay(inMilisegunds)" 
          antes de obtener las categorías. Este retraso controla la frecuencia con la que se obtienen las categorías. 
          Luego, se utiliza "yield return" para devolver las categorías obtenidas a través del método "_repository.GetAll()".
        */
        public async IAsyncEnumerable<List<Category>> Emit([EnumeratorCancellation] CancellationToken token, int inMilliseconds = 500)
        {
            while (token.IsCancellationRequested.Equals(false))
            {
                await Task.Delay(inMilliseconds);
                yield return _repository.GetAll();
            }

        }
    }
}
