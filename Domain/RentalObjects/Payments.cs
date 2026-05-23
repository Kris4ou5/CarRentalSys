using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSys.Domain.Entities;
using CarRentalSys.Domain.Enums;

namespace CarRentalSys.Domain.RentalObjects
{
    public class Payments
    {
        public int Id { get; private set; }
        public int RentalId { get; private set; }
        public CarCategory CarCategory { get; private set; }
        public decimal Amount { get; private set; }

        public Rentals Rental { get; private set; }

        public Payments() { }

         public Payments(int rentalId, CarCategory category, int amount)
         {
            
            RentalId = rentalId;
            CarCategory = category;
            Amount = amount;
         }



    }
}
