using CarRentalSys.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Car(int id, string brand, string model, CarCategory category, decimal pricePerDay) 
        {
            Id = id;
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
