using DShop.Common.Messages;
using System;

namespace Contracts.Commands
{
    [MessageNamespace("flights")]
    public class BookFlight : ICommand
    {
        public DateTime From { get;  }
        public DateTime To { get; }
        public Guid UserId { get; }

        public BookFlight(DateTime from, DateTime to, Guid userId)
            => (From, To, UserId) = (from, to, userId);
    }
}
