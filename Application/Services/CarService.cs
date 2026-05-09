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
        private readonly IRentalRepository _rentalRepo;

        public CarService(ICarRepository carRepo, IRentalRepository rentalRepo)
        {
            _carRepo = carRepo;
            _rentalRepo = rentalRepo;

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

        public List<Car> GetAvailableCars()
        {
            var cars = _carRepo.GetAll();

            return cars
                .Where(c => c.Status == CarStatus.Available)
                .ToList();
        }

        public List<Car> GetAvailableCarsForPeriod(DateTime start, DateTime end)
        {
            var cars = GetAvailableCars();
            var rentals = _rentalRepo.GetAll();
            List<Car> result = new List<Car>();
            bool carOverLap = false;
            foreach (var car in cars)
            {
                carOverLap = false;
                foreach (var rental in rentals)
                {
                    
                    if (car.Id  == rental.CarId)
                    {
                        if(rental.StartDate <= end && rental.EndDate >= start)
                        {
                            carOverLap = true;
                        }
                    }

                    if (!carOverLap)
                    {
                        result.Add(car);
                    }
                }
            }
            return result;
        }

        public bool IsCarAvailable(int carId)
        {
            var car = _carRepo.GetById(carId);

            if (car == null) return false;

            return car.Status == CarStatus.Available;
        }

        public Car GetCarById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid car ID");

            return _carRepo.GetById(id);
        }


    }
}

