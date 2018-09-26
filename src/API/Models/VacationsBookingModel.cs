using System;

namespace API.Models
{
    public class VacationsBookingModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid TransactionId { get; set; }
        public Guid? UserId { get; set; }
    }
}
