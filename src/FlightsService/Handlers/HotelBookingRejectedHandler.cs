using Contracts.Events;
using DShop.Common.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DShop.Common.RabbitMq;

namespace FlightsService.Handlers
{
    public class HotelBookingRejectedHandler : IEventHandler<HotelBookingRejected>
    {
        public Task HandleAsync(HotelBookingRejected @event, ICorrelationContext context)
        {
            Console.WriteLine("FALL BACK!");
            return Task.CompletedTask;
        }
    }
}
