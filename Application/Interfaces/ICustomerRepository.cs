using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSys.Domain.Entities;

namespace CarRentalSys.Application.Interfaces
{
    public interface ICustomerRepository
    {
        public IReadOnlyList<Customer> GetAll();
        public Customer GetById(int id);
        public void Save(Customer newCustomer);
    }
}
