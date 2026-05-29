using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSys.Application.Interfaces;
using CarRentalSys.Domain.Entities;

namespace CarRentalSys.Infrastructure.EFData
{
    public class EFCarsRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        public EFCarsRepository(AppDbContext context)
        {
            _context = context;
        }

        public IReadOnlyList<Car> GetAll()
        {
            var cars = _context.Cars.ToList();

            return cars;
        }

        public Car GetById(int id)
        {
            var car = _context.Cars.FirstOrDefault(x => x.Id == id);
            if (car == null) throw new Exception("Car not found");
            return car;
        }

        public void Save(Car newCar)
        {
            if (newCar == null) throw new Exception("Car cannot be null");
            var existingCar = _context.Cars.FirstOrDefault(x => x.Id == newCar.Id);
            if (existingCar != null)
            {
                existingCar.ChangeStatus(newCar.Status);     
            }
            else _context.Cars.Add(newCar);

            
            _context.SaveChanges();
        }
    }
}
