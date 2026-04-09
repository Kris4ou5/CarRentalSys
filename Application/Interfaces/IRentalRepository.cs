using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSys.Domain.Entities;

namespace CarRentalSys.Application.Interfaces
{
    public interface IRentalRepository
    {
        public IReadOnlyList<Rentals> GetAll();
        public Rentals GetById(int id);
        public void Save(Rentals newRental);
    }
}
