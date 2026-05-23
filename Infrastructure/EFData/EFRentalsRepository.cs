using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSys.Application.Interfaces;
using CarRentalSys.Domain.Entities;

namespace CarRentalSys.Infrastructure.EFData
{
    internal class EFRentalsRepository : IRentalRepository
    {
        public readonly AppDbContext _context;

        public EFRentalsRepository(AppDbContext conetext)
        {
            _context = conetext;
        }
        public IReadOnlyList<Rentals> GetAll()
        {
           return _context.Rentals.ToList();
        }

        public Rentals GetById(int id)
        {
            var rental = _context.Rentals.FirstOrDefault(x => x.Id == id);
            if (rental == null) throw new Exception("Rental not found");
            return rental;

        }

        public void Save(Rentals newRental)
        {
            if (newRental == null) throw new Exception("Rental cant be null");
            _context.Rentals.Add(newRental);
            _context.SaveChanges();
        }
    }
}
