using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelsService.Domain
{
    public class Hotel
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public int Capacity { get; protected set; }
        public List<Guid> ResidentIds { get; protected set; }
        public Guid? TransactionId { get; protected set; }

        public bool IsAvailable => ResidentIds.Count() < Capacity;
        public bool IsLocked => TransactionId.HasValue;

        public Hotel(string name, int capacity)
            => (Id, Name, Capacity, ResidentIds) = (Guid.NewGuid(), name, capacity,  new List<Guid>());

        public void Book(Guid userId)
        {
            if(!IsAvailable)
            {
                throw new Exception("Cannot book this hotel!");
            }

            ResidentIds.Add(userId);
        }

        public void Lock(Guid transactionId)
        {
            if (IsLocked)
            {
                throw new Exception("Cannot lock this hotel!");
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
