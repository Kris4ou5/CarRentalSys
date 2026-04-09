using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSys.Domain.Entities;

namespace CarRentalSys.Application.Interfaces
{
    public interface ICarRepository
    {
        public IReadOnlyList<Car> GetAll();
        public Car GetById(int id);
        public void Save(Car newCar);
    }
}
