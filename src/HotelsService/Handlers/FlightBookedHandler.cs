using Contracts.Events;
using DShop.Common.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DShop.Common.RabbitMq;
using HotelsService.Domain;

namespace HotelsService.Handlers
{
    public class FlightBookedHandler : IEventHandler<FlightBooked>
    {
        private readonly IHandler _handler;
        private readonly IBusPublisher _busPublisher;
        private readonly IEnumerable<Hotel> _repository;

        private Hotel _bookedHotel;

        public FlightBookedHandler(IHandler handler, IBusPublisher busPublisher)
        {
            _handler = handler;
            _busPublisher = busPublisher;

            _repository = new List<Hotel>
            {
                new Hotel(name: "Hilton", capacity: 2800),
                new Hotel(name: "Novotel", capacity: 10050),
                new Hotel(name: "Super hotel", capacity: 10000)
            };
        }

        public async Task HandleAsync(FlightBooked @event, ICorrelationContext context)
            => await _handler
                .Handle(async () =>
                {                    
                    var availableHotel = _repository.FirstOrDefault(h => h.IsAvailable && !h.IsLocked);

                    if (availableHotel is null)
                    {
                        throw new Exception("No hotel available");
                    }

                    availableHotel.Book(@event.UserId);
                    _bookedHotel = availableHotel;
                })
                .OnSuccess(async () =>
                {
                    await _busPublisher.PublishAsync(new HotelBooked(_bookedHotel.Id, @event.From, @event.To, @event.UserId), context);
                })
                .OnError(async (ex) => 
                {
                    await _busPublisher.PublishAsync(new HotelBookingRejected(@event.From, @event.To, @event.UserId), context);
                })
                .ExecuteAsync();
    }
}
