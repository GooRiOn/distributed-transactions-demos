using API.Models;
using API.Services;
using RestEase;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Coordinators
{
    public class VacationBookingCoordinator : IVacationBookingCoordinator
    {
        private readonly ICarsService _carsService;
        private readonly IHotelsService _hotelsService;
        private readonly IFlightsService _flightsService;


        public VacationBookingCoordinator()
        {
            _carsService = RestClient.For<ICarsService>("http://localhost:5001");
            _hotelsService = RestClient.For<IHotelsService>("http://localhost:5003");
            _flightsService = RestClient.For<IFlightsService>("http://localhost:5002");
        }

        public async Task<bool> BookAsync(DateTime from, DateTime to, Guid userId)
        {
            var transactionId = Guid.NewGuid();

            var commitRequestTasks = new Task<bool>[3]
            {
                _carsService.CheckAvailablityAsync(from, to, transactionId),
                _hotelsService.CheckAvailablityAsync(from, to, transactionId),
                _flightsService.CheckAvailablityAsync(from, to, transactionId),
            };

            await Task.WhenAll(commitRequestTasks);

            var isSucceeded = commitRequestTasks.All(t => t.Result);

            if(! isSucceeded)
            {
                await UnlockAsync(transactionId);
                return false;
            }

            var model = new VacationsBookingModel
            {
                From = from,
                To = to,
                TransactionId = transactionId,
                UserId = userId
            };

            var commitRequests = new Task[3]
            {
                _carsService.RentAsync(model),
                _hotelsService.BookAsync(model),
                _flightsService.BookAsync(model),
            };

            await Task.WhenAll(commitRequests);
            return true;
        }

        private async Task UnlockAsync(Guid transactionId)
        {
            var unlockTasks = new Task[3]
            {
                _carsService.UnlockAsync(transactionId),
                _hotelsService.UnlockAsync(transactionId),
                _flightsService.UnlockAsync(transactionId),
            };

            await Task.WhenAll(unlockTasks);
        }
    }
}
