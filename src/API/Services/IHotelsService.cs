using API.Models;
using RestEase;
using System;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IHotelsService
    {
        [Get("/api/HotelsBooking/Check")]
        Task<bool> CheckAvailablityAsync(DateTime from, DateTime to, Guid transactionId);

        [Post("/api/HotelsBooking/Book")]
        Task BookAsync([Body] VacationsBookingModel model);

        [Post("/api/HotelsBooking/{transactionId}/Unlock")]
        Task UnlockAsync([Path] Guid transactionId);
    }
}
