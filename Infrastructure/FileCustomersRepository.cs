using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSys.Application.Interfaces;
using CarRentalSys.Domain.Entities;
using NUnit.Framework.Constraints;

namespace CarRentalSys.Infrastructure
{
    public class FileCustomersRepository : ICustomerRepository
    {
        private readonly FileStorage _storage;

        public FileCustomersRepository(FileStorage storage)
        {
            _storage = storage;
        }

        public IReadOnlyList<Customer> GetAll()
        {
            var saved = _storage.Load();
            return saved.Customers;
        }

        public Customer GetById(int id)
        {
            var saved = _storage.Load();

            foreach (var customer in saved.Customers)
            {
                if (customer.Id == id)
                    return customer;
            }

            throw new ArgumentException($"No customer with ID:{id} found ");
        }

        public void Save(Customer newCustomer)
        {
            var saved = _storage.Load();

            if (newCustomer.Id == 0)
            {
                var customer = new Customer(
                    saved.NextID,
                    newCustomer.FullName,
                    newCustomer.DriverLicenseNumber,
                    newCustomer.Phone,
                    newCustomer.Email
                );

                saved.Customers.Add(customer);
                saved.NextID++;
            }
            else
            {
                bool found = false;

                for (int i = 0; i < saved.Customers.Count; i++)
                {
                    if (saved.Customers[i].Id == newCustomer.Id)
                    {
                        saved.Customers[i] = newCustomer;
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
