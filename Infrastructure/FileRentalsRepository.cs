using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSys.Application.Interfaces;
using CarRentalSys.Domain.Entities;

namespace CarRentalSys.Infrastructure
{
    public class FileRentalsRepository : IRentalRepository
    {
        private readonly FileStorage _storage;

        public FileRentalsRepository(FileStorage storage)
        {
            _storage = storage;
        }

        public IReadOnlyList<Rentals> GetAll()
        {
            var saved = _storage.Load();

            return saved.Rentals;
        }

        public Rentals GetById(int id)
        {
            var saved = _storage.Load();

            foreach(var rental in saved.Rentals)
            {
                if(id  == rental.Id)
                    return rental;
                
            }

            throw new ArgumentException($"No rental with ID:{id} found");

        }

        public void Save(Rentals newRental)
        {
            var saved = _storage.Load();

            if(newRental.Id == 0)
            {
                var correctedRental = new Rentals(saved.NextID, newRental.CarId, newRental.CustomerId, newRental.StartDate, newRental.EndDate);

            }
            else
            {
                 bool idFound = false;

                for(int i = 0; i < saved.Rentals.Count; i++)
                {
                    if (saved.Rentals[i].Id == newRental.Id)
                    {
                        saved.Rentals[i] = newRental;
                        idFound = true;
                        break;
                    }
                }

                if (!idFound)
                {
                    throw new Exception("ID out of range");
                }
            }
            _storage.Save(saved);
        }

    }
}
