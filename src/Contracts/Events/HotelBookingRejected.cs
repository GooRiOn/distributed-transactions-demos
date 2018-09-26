using DShop.Common.Messages;
using System;

namespace Contracts.Events
{
    [MessageNamespace("flights")]
    public class HotelBookingRejected : IRejectedEvent
    {
        public string Reason => string.Empty;
        public string Code => string.Empty;
        
        public DateTime From { get; }
        public DateTime To { get; }
        public Guid UserId { get; }

        public HotelBookingRejected(DateTime from, DateTime to, Guid userId)
            => (From, To, UserId) = (from, to, userId);
    }
}
