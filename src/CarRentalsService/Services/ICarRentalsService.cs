using System;
using System.Threading.Tasks;

namespace CarsService.Services
{
    public interface ICarRentalsService
    {
        Task<bool> CheckAvailabilityAsync(DateTime from, DateTime to, Guid transactionId);
        Task RentAsync(DateTime from, DateTime to, Guid transactionId, Guid userId);
        Task UnlockAsync(Guid transactionId);
    }
}
