using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Coordinators;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        public Guid UserId => Guid.NewGuid();

        private readonly IVacationBookingCoordinator _coordinator;

        public BookingController(IVacationBookingCoordinator coordinator)
            => _coordinator = coordinator;

        [HttpPost("")]
        public Task<bool> BooksAsync([FromBody] Model model)
            => _coordinator.BookAsync(model.From, model.To, UserId);

        public class Model
        {
            public DateTime From { get; set; }
            public DateTime To { get; set; }
        }
    }
}
