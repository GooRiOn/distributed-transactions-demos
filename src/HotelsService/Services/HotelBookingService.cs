using HotelsService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsService.Services
{
    public sealed class HotelBookingService : IHotelBookingService
    {
        private readonly IEnumerable<Hotel> _repository;

        public HotelBookingService()
        {
            _repository = new List<Hotel>
            {
                new Hotel(name: "Hilton", capacity: 2800),
                new Hotel(name: "Novotel", capacity: 10050),
                new Hotel(name: "Super hotel", capacity: 10000)
            };
        }

        public async Task<bool> CheckAvailabilityAsync(DateTime from, DateTime to, Guid transactionId)
        {
            var availableHotel = _repository.FirstOrDefault(h => h.IsAvailable && !h.IsLocked);

            if (availableHotel is null)
            {
                return await Task.FromResult(false);
            }

            availableHotel.Lock(transactionId);
            return await Task.FromResult(true);
        }

        public Task BookAsync(Guid transactionId, Guid userId)
        {
            var hotel = _repository.FirstOrDefault(h => h.TransactionId == transactionId);
            hotel.Book(userId);
            hotel.Unlock(transactionId);

            return Task.CompletedTask;
        }

        public Task UnlockAsync(Guid transactionId)
        {
            var hotel = _repository.FirstOrDefault(h => h.TransactionId == transactionId);
            hotel?.Unlock(transactionId);
            return Task.CompletedTask;
        }
    }
}
