using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelsService.Services;

namespace HotelsService.Controllers
{
    [Route("api/[controller]")]
    public class HotelsBookingController : Controller
    {
        private readonly IHotelBookingService _service;

        public HotelsBookingController(IHotelBookingService service)
            => _service = service;

        [HttpGet("Check")]
        public async Task<bool> CheckAvailabilityAsync([FromQuery] Model model)
            => await _service.CheckAvailabilityAsync(model.From, model.To, model.TransactionId);

        [HttpPost("Book")]
        public async Task BookAsync([FromBody] Model model)
            => await _service.BookAsync(model.TransactionId, model.UserId ?? Guid.NewGuid());

        [HttpPost("{transactionId}/Unlock")]
        public async Task UnlockAsync(Guid transactionId)
            => await _service.UnlockAsync(transactionId);

        public class Model
        {
            public DateTime From { get; set; }
            public DateTime To { get; set; }
            public Guid TransactionId { get; set; }
            public Guid? UserId { get; set; }
        }
    }
}
