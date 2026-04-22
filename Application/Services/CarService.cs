using CarRentalSys.Application.Interfaces;
using CarRentalSys.Domain.Entities;
using CarRentalSys.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSys.Application.Services
{   
    public class CarService
    {
        private readonly ICarRepository _carRepo;

        public CarService(ICarRepository carRepo)
        {
            _carRepo = carRepo;
        }

        public Car AddCar(string brand, string model, CarCategory category, decimal pricePerDay)
        {
            if (string.IsNullOrWhiteSpace(brand) || string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Invalid car data");

            if (pricePerDay <= 0)
                throw new ArgumentException("Price must be positive");

            var car = new Car(0, brand, model, category, pricePerDay);
            
            _carRepo.Save(car);

            return car;
        }

        public void ChangeCarStatus(int carId, CarStatus newStatus)
        {
            var car = _carRepo.GetById(carId);
            if (car == null) throw new KeyNotFoundException($"Car with ID {carId} not found.");

            car.ChangeStatus(newStatus);
            _carRepo.Save(car);
        }

        public void SendToMaintenance(int carId)
        {
            ChangeCarStatus(carId, CarStatus.InMaintenance);
        }

        public IReadOnlyList<Car> GetAllCars()
        {
            return _carRepo.GetAll();
        }

    }
}

