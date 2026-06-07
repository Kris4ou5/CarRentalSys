using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSys.Application.Interfaces;
using CarRentalSys.Domain.Entities;

namespace CarRentalSys.Infrastructure.EFData
{
    public class EFCustomersRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public EFCustomersRepository(AppDbContext context)
        {
            _context = context;
        }
        public IReadOnlyList<Customer> GetAll()
        {
            return _context.Customer.ToList();
        }

        public Customer GetById(int id)
        {
            var customer = _context.Customer.FirstOrDefault(x => x.Id == id);
            if (customer == null) throw new Exception("Customer not found");
            return customer;
        }

        public void Save(Customer newCustomer)
        {
            if (newCustomer == null) throw new Exception("Customer cant be null");
            var existingCustomer = _context.Customer.FirstOrDefault(x => x.Id == newCustomer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.UpdateContactInfo(newCustomer.FullName,newCustomer.DriverLicenseNumber,newCustomer.Phone, newCustomer.Email);
                
            }
            else _context.Customer.Add(newCustomer);
            
            _context.SaveChanges();
        }
    }
}
