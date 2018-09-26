using CarsService.Domain;
using Contracts.Events;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsService.Handlers
{
    public class HotelBookedHandler : IEventHandler<HotelBooked>
    {
        private readonly IHandler _handler;
        private readonly IBusPublisher _busPublisher;
        private readonly IEnumerable<Car> _repository;
        

        public HotelBookedHandler(IHandler handler, IBusPublisher busPublisher)
        {
            _handler = handler;
            _busPublisher = busPublisher;

            _repository = new List<Car>
            {
                new Car(brand: "Audi", vehicleNumber: "GVYGF3243DFGYG2433"),
                new Car(brand: "Toyota", vehicleNumber: "DFREVVVV%9797KIO"),
                new Car(brand: "Mazda", vehicleNumber: "VV676FHT5456JHTYH"),
            };
        }

        public async Task HandleAsync(HotelBooked @event, ICorrelationContext context)
            => await _handler
                .Handle(async () =>
                {
                    var availableCar = _repository.FirstOrDefault(c => !c.IsLocked && !c.IsRented);

                    if (availableCar is null)
                    {
                        throw new Exception("No car available");
                    }

                    availableCar.Rent(@event.From, @event.To);
                })
                .ExecuteAsync();
    }
}
