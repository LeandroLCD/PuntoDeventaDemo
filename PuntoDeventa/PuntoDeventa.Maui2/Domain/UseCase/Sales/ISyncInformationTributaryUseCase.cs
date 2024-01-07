using PuntoDeventa.UI.Sales.State;
using System.Threading.Tasks;

namespace PuntoDeventa.Domain.UseCase.Sales
{
    public interface ISyncInformationTributaryUseCase
    {
        Task<SalesState> Sync();
    }
}
