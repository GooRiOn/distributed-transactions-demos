using FlightsService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightsService.Services
{
    public sealed class FlightBookingService : IFlightsBookingService
    {
        private readonly IEnumerable<Flight> _repository;

        public FlightBookingService()
        {
            _repository = new List<Flight>
            {
                new Flight(flightNumber: "HTY7865", date: new DateTime(2018, 8, 10, 10, 30, 0), capacity: 200),
                new Flight(flightNumber: "HTY7865", date: new DateTime(2018, 8, 11, 10, 30, 0), capacity: 150),
                new Flight(flightNumber: "HTY7865", date: new DateTime(2018, 8, 12, 10, 30, 0), capacity: 100)
            };
        }

        public async Task<bool> CheckAvailabilityAsync(DateTime from, DateTime to, Guid transactionId)
        {
            var availableFlight = _repository.FirstOrDefault(f => f.IsAvailable && f.Date >= from && f.Date <= to);

            if (availableFlight is null)
            {
                return await Task.FromResult(false);
            }

            availableFlight.Lock(transactionId);
            return await Task.FromResult(true);
        }

        public Task BookAsync(Guid transactionId, Guid userId)
        {
            var flight = _repository.FirstOrDefault(f => f.TransactionId == transactionId);
            flight.Book(userId);
            flight.Unlock(transactionId);

            return Task.CompletedTask;
        }

        public Task UnlockAsync(Guid transactionId)
        {
            var flight = _repository.FirstOrDefault(f => f.TransactionId == transactionId);            
            flight?.Unlock(transactionId);
            return Task.CompletedTask;
        }
    }
}
