using DShop.Common.Messages;
using System;

namespace Contracts.Events
{
    [MessageNamespace("hotels")]
    public class FlightBooked : IEvent
    {
        public Guid Id { get; }
        public DateTime From { get; }
        public DateTime To { get; }
        public Guid UserId { get; }

        public FlightBooked(Guid id, DateTime from, DateTime to, Guid userId)
            => (Id, From, To, UserId) = (id, from, to, userId);
    }
}
