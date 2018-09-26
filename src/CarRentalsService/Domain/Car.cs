using System;

namespace CarsService.Domain
{
    public class Car
    {
        public Guid Id { get; protected set; }
        public string Brand { get; protected set; }
        public string VehicleNumber { get; protected set; }
        public DateTime? RendetFrom { get; protected set; }
        public DateTime? RendetTo { get; protected set; }
        public Guid? TransactionId { get; set; }

        public bool IsRented => RendetFrom.HasValue && RendetTo.HasValue;
        public bool IsLocked => TransactionId.HasValue;

        public Car(string brand, string vehicleNumber)
            => (Id, Brand, VehicleNumber) = (Guid.NewGuid(), brand, vehicleNumber);

        public void Rent(DateTime from, DateTime to)
        {
            if(IsRented)
            {
                throw new Exception("Cannot rent this car!");
            }

            RendetFrom = from;
            RendetTo = to;
        }

        public void Lock(Guid transactionId)
        {
            if (IsLocked)
            {
                throw new Exception("Cannot lock this car!");
            }

            TransactionId = transactionId;
        }

        public void Unlock(Guid transactionId)
        {
            if(IsLocked && TransactionId == transactionId)
            {
                TransactionId = null;
            }
        }
    }
}
