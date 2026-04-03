using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSys.Domain.Entities;

namespace CarRentalSys.Infrastructure
{
    public class FileCarsRepository
    {
        private readonly FileStorage _storage;

        public FileCarsRepository(FileStorage storage)
        {
            _storage = storage;
        }

        public IReadOnlyList<Car> GetAll()
        {
            var saved = _storage.Load();
            return saved.AllCars;
        }

        public Car GetById(int id)
        {
            var saved = _storage.Load();

            foreach(var car in saved.AllCars)
            {
                if (car.Id == id)
                    return car;
            }

            throw new ArgumentException($"No car with ID: {id} found");
        }

        public void Save(Car newCar)
        {
            var saved = _storage.Load();

            if (newCar.Id == 0)
            {
                var car = new Car(
                    saved.NextID,
                    newCar.Brand,
                    newCar.Model,
                    newCar.Category,
                    newCar.PricePerDay
                );

                saved.AllCars.Add(car);
                saved.NextID++;
            }
            else
            {
                bool found = false;

                for (int i = 0; i < saved.AllCars.Count; i++)
                {
                    if (saved.AllCars[i].Id == newCar.Id)
                    {
                        saved.AllCars[i] = newCar;
                        found = true;
                        break;
                    }
                }

                if (!found)
                    throw new Exception("ID out of range");
            }

            _storage.Save(saved);
        }
    }
}
