using System.Threading.Tasks;

namespace Alojamiento.Business.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ProcessPaymentAsync(int reservaId, decimal amount, string currency);
    }
}
