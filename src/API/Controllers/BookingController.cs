using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Coordinators;
using DShop.Common.RabbitMq;
using Contracts.Commands;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        public Guid UserId => Guid.NewGuid();

        private readonly IVacationBookingCoordinator _coordinator;
        private readonly IBusPublisher _busPublisher;

        public BookingController(IVacationBookingCoordinator coordinator, IBusPublisher busPublisher)
            => (_coordinator, _busPublisher) = (coordinator, busPublisher);

        [HttpPost("2PC")]
        public Task<bool> Book2PCAsync([FromBody] Model model)
            => _coordinator.BookAsync(model.From, model.To, UserId);

        [HttpPost("Chronology")]
        public Task BooksAsync([FromBody] Model model)
            => _busPublisher.SendAsync(new BookFlight(model.From, model.To, UserId), CorrelationContext.Empty);

        public class Model
        {
            public DateTime From { get; set; }
            public DateTime To { get; set; }
        }
    }
}
