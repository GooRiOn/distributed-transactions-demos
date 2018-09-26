using API.Models;
using RestEase;
using System;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IFlightsService
    {
        [Get("/api/FlightsBooking/Check")]
        Task<bool> CheckAvailablityAsync(DateTime from, DateTime to, Guid transactionId);

        [Post("/api/FlightsBooking/Book")]
        Task BookAsync([Body] VacationsBookingModel model);

        [Post("/api/FlightsBooking/{transactionId}/Unlock")]
        Task UnlockAsync([Path] Guid transactionId);
    }
}
