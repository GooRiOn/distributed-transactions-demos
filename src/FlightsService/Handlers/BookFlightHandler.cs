using Contracts.Commands;
using DShop.Common.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DShop.Common.RabbitMq;
using FlightsService.Domain;
using Contracts.Events;

namespace FlightsService.Handlers
{
    public sealed class BookFlightHandler : ICommandHandler<BookFlight>
    {
        private readonly IHandler _handler;
        private readonly IBusPublisher _busPublisher;
        private readonly IEnumerable<Flight> _repository;

        private Flight _bookedFlight;

        public BookFlightHandler(IHandler handler, IBusPublisher busPublisher)
        {
            _handler = handler;
            _busPublisher = busPublisher;

            _repository = new List<Flight>
            {
                new Flight(flightNumber: "HTY7865", date: new DateTime(2018, 8, 10, 10, 30, 0), capacity: 200),
                new Flight(flightNumber: "HTY7865", date: new DateTime(2018, 8, 11, 10, 30, 0), capacity: 150),
                new Flight(flightNumber: "HTY7865", date: new DateTime(2018, 8, 12, 10, 30, 0), capacity: 100)
            };
        }

        public async Task HandleAsync(BookFlight command, ICorrelationContext context)
            => await _handler
                .Handle(async () =>
                {
                    var availableFlight = _repository.FirstOrDefault(f => f.IsAvailable && f.Date >= command.From && f.Date <= command.To);

                    if (availableFlight is null)
                    {
                        throw new Exception("No flight availabke");
                    }

                    availableFlight.Book(command.UserId);
                    _bookedFlight = availableFlight;
                })
                .OnSuccess(async () => 
                {
                    await _busPublisher.PublishAsync(new FlightBooked(_bookedFlight.Id, command.From, command.To, command.UserId), context);
                })
                .ExecuteAsync();
    }
}
