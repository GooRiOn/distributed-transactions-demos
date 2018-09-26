using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using CarsService.Services;

namespace CarsService.Controllers
{
    [Route("api/[controller]")]
    public class CarRentalsController : Controller
    {
        private readonly ICarRentalsService _service;

        public CarRentalsController(ICarRentalsService service)
            => _service = service;

        [HttpGet("Check")]
        public async Task<bool> CheckAvailabilityAsync([FromQuery] Model model)
            => await _service.CheckAvailabilityAsync(model.From, model.To, model.TransactionId);

        [HttpPost("Rent")]
        public async Task RentAsync([FromBody] Model model)
            => await _service.RentAsync(model.From, model.To, model.TransactionId, model.UserId ?? Guid.NewGuid());

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
