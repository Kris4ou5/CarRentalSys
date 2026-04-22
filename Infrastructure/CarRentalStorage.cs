using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSys.Domain.Enums;
using CarRentalSys.Domain.Entities;
using CarRentalSys.Domain.RentalObjects;

namespace CarRentalSys.Infrastructure
{
    public class CarRentalStorage
    {
        public int NextID { get; set; } = 1;

        public List<Car> AllCars { get; set; } = new List<Car>();
        public List<Rentals> Rentals { get; set; } = new List<Rentals>();
        public List<Customer> Customers { get; set; }    = new List<Customer>();
    }
}
