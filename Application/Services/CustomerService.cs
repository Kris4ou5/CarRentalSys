using CarRentalSys.Application.Interfaces;
using CarRentalSys.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSys.Application.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IRentalRepository _rentalRepository;
        public CustomerService(ICustomerRepository customerRepository, IRentalRepository rentalRepo)
        {
            this._customerRepository = customerRepository;
            this._rentalRepository = rentalRepo;
        }

        public void RegisterCustomer(string name, string driveLcs, string phone, string email)
        {
            var newCustomer = new Customer(0, name, driveLcs, phone, email);
            ValidateCustomer(newCustomer);
            _customerRepository.Save(newCustomer);
        }

        public void EditCustomer(Customer customer)
        {
            ValidateCustomer(customer);
            _customerRepository.Save(customer);
        }

        public IReadOnlyList<Rentals> GetCustomerRentalHistory(int customerid)
        {
            var customer = _customerRepository.GetById(customerid);
            ValidateCustomer(customer);
            var rentals = _rentalRepository.GetAll();
            List<Rentals> customerRentals = new List<Rentals>();
            foreach(var rental in rentals)
            {
                if(rental.CustomerId == customer.Id)
                {
                    customerRentals.Add(rental);
                }
            }
            return customerRentals;
        }

        private void ValidateCustomer(Customer customer)
        {
            if (customer == null) throw new ArgumentException("Клиент не валиден");
            if (string.IsNullOrEmpty(customer.FullName)) throw new ArgumentException("Невалидно име.");
            if (string.IsNullOrEmpty(customer.DriverLicenseNumber) || customer.DriverLicenseNumber.Length < 9 || customer.DriverLicenseNumber.Length > 9) throw new ArgumentException("Невалиден номер на книжка.");
            if (string.IsNullOrEmpty(customer.Phone)) throw new ArgumentException("Невалиден телефонен номер.");
        }
    }
}
