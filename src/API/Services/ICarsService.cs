using API.Models;
using RestEase;
using System;
using System.Threading.Tasks;

namespace API.Services
{
    public interface ICarsService
    {
        [Get("/api/CarRentals/Check")]
        Task<bool> CheckAvailablityAsync(DateTime from, DateTime to, Guid transactionId);

        [Post("/api/CarRentals/Rent")]
        Task RentAsync([Body] VacationsBookingModel model);

        [Post("/api/CarRentals/{transactionId}/Unlock")]
        Task UnlockAsync([Path] Guid transactionId);
    }
}
