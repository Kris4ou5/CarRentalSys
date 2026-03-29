using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSys.Domain.Enums;

namespace CarRentalSys.Domain.RentalObjects
{
    public class Payments
    {
        public int Id { get; private set; }
        public int RentalId { get; private set; }
        public CarCategory CarCategory { get; private set; }
        public decimal Amount { get; private set; }

         public Payments(int id, int rentalId, CarCategory category, int amount)
         {
            Id = id;
            RentalId = rentalId;
            CarCategory = category;
            Amount = amount;
        }



    }
}
