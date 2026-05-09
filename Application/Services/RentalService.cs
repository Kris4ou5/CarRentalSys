using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSys.Application.Interfaces;
using CarRentalSys.Domain.Entities;

namespace CarRentalSys.Application.Services
{
    public class RentalService
    {
        private readonly ICarRepository _carRepo;
        private readonly IRentalRepository _rentalRepo;
        private readonly ICustomerRepository _customerRepo;

        public RentalService(ICarRepository carRepo, IRentalRepository rentalRepo, ICustomerRepository customerRepo)
        {
            _carRepo = carRepo;
            _rentalRepo = rentalRepo;
            _customerRepo = customerRepo;
        }

        public decimal Price(Car car, int days, int promo)
        {
            decimal discount = promo / 100m;
            decimal categoryMulti = (decimal)car.Category/10 + 1;
            decimal price = car.PricePerDay * days * categoryMulti;
            price -= price * discount;
            return price;
        }



    }
}
