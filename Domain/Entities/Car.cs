using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using CarRentalSys.Domain.Enums;
using Microsoft.IdentityModel.Tokens;

namespace CarRentalSys.Domain.Entities
{
    public class Car
    {
        public int Id { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public CarCategory Category { get; private set; }
        public decimal PricePerDay { get; private set; }
        public CarStatus Status { get; private set; }

        public ICollection<Rentals> Rentals { get; private set; } = new List<Rentals>();

        public Car() { }

        public Car( string brand, string model, CarCategory category, decimal pricePerDay) 
        {
            if (brand.IsNullOrEmpty()) throw new ArgumentNullException("You must set brand");
            if (model.IsNullOrEmpty()) throw new ArgumentNullException("You must set drivers license");
            if (pricePerDay <0) throw new ArgumentNullException("Price per day must be positive");
            Brand = brand;
            Model = model;
            Category = category;
            PricePerDay = pricePerDay;
            Status = CarStatus.Available;
        }

        public void ChangeStatus(CarStatus newStatus) 
        {
            Status = newStatus;
        }

    }
}
