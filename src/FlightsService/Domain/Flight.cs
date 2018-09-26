using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightsService.Domain
{
    public class Flight
    {
        public Guid Id { get; protected set; }
        public string FlightNumber { get; protected set; }
        public DateTime Date { get; protected set; }
        public int Capacity { get; protected set; }
        public List<Guid> PassengerIds { get; protected set; }
        public Guid? TransactionId { get; protected set; }

        public bool IsAvailable => PassengerIds.Count() < Capacity;
        public bool IsLocked => TransactionId.HasValue;

        public Flight(string flightNumber, DateTime date, int capacity)
            => (Id, FlightNumber, Date, Capacity, PassengerIds) = (Guid.NewGuid(), flightNumber, date, capacity, new List<Guid>());

        public void Book(Guid userId)
        {
            if (!IsAvailable)
            {
                throw new Exception("Cannot book this flight!");
            }

            PassengerIds.Add(userId);
        }

        public void Lock(Guid transactionId)
        {
            if (IsLocked)
            {
                throw new Exception("Cannot lock this flight!");
            }

            TransactionId = transactionId;
        }

        public void Unlock(Guid transactionId)
        {
            if (IsLocked && TransactionId == transactionId)
            {
                TransactionId = null;
            }
        }
    }
}
