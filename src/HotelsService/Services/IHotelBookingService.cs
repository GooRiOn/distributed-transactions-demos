using System;
using System.Threading.Tasks;

namespace HotelsService.Services
{
    public interface IHotelBookingService
    {
        Task<bool> CheckAvailabilityAsync(DateTime from, DateTime to, Guid transactionId);
        Task BookAsync(Guid transactionId, Guid userId);
        Task UnlockAsync(Guid transactionId);
    }
}
