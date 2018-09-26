using System;
using System.Threading.Tasks;

namespace FlightsService.Services
{
    public interface IFlightsBookingService
    {
        Task<bool> CheckAvailabilityAsync(DateTime from, DateTime to, Guid transactionId);
        Task BookAsync(Guid transactionId, Guid userId);
        Task UnlockAsync(Guid transactionId);
    }
}
