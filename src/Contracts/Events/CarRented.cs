using DShop.Common.Messages;
using System;

namespace Contracts.Events
{
    public class CarRented : IEvent
    {
        public Guid Id { get; }
        public DateTime From { get; }
        public DateTime To { get; }
        public Guid UserId { get; }

        public CarRented(Guid id, DateTime from, DateTime to, Guid userId)
            => (Id, From, To, UserId) = (id, from, to, userId);
    }
}
