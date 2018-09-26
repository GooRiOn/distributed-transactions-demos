using CarsService.Domain;
using CarsService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsService.Services
{
    public sealed class CarRentalsService : ICarRentalsService
    {
        private readonly IEnumerable<Car> _repository;

        public CarRentalsService()
        {
            _repository = new List<Car>
            {
                new Car(brand: "Audi", vehicleNumber: "GVYGF3243DFGYG2433"),
                new Car(brand: "Toyota", vehicleNumber: "DFREVVVV%9797KIO"),
                new Car(brand: "Mazda", vehicleNumber: "VV676FHT5456JHTYH"),
            };
        }

        public async Task<bool> CheckAvailabilityAsync(DateTime from, DateTime to, Guid transactionId)
        {
            var availableCar = _repository.FirstOrDefault(c => !c.IsLocked && !c.IsRented);

            if(availableCar is null)
            {
                return await Task.FromResult(false);
            }

            availableCar.Lock(transactionId);
            return await Task.FromResult(true);
        }

        public Task RentAsync(DateTime from, DateTime to, Guid transactionId, Guid userId)
        {
            var car = _repository.FirstOrDefault(c => c.TransactionId == transactionId);

            car.Rent(from, to);
            car.Unlock(transactionId);

            return Task.CompletedTask;
        }

        public Task UnlockAsync(Guid transactionId)
        {
            var car = _repository.FirstOrDefault(c => c.TransactionId == transactionId);
            car?.Unlock(transactionId);
            return Task.CompletedTask;
        }
    }
}
