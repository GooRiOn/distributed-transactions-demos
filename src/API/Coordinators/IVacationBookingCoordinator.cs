using System;
using System.Threading.Tasks;

namespace API.Coordinators
{
    public interface IVacationBookingCoordinator
    {
        Task<bool> BookAsync(DateTime from, DateTime to, Guid userId);
    }
}
