using DShop.Common.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Events
{
    [MessageNamespace("cars")]
    public class HotelBooked : IEvent
    {
        public Guid Id { get; }
        public DateTime From { get; }
        public DateTime To { get; }
        public Guid UserId { get; }

        public HotelBooked(Guid id, DateTime from, DateTime to, Guid userId)
            => (Id, From, To, UserId) = (id, from, to, userId);
    }
}
