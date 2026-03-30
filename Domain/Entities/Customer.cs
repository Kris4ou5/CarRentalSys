using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSys.Domain.Entities
{
    public class Customer
    {
        public int Id { get; private set; }
        public string FullName { get; private set; }
        public string DriverLicenseNumber { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; } = string.Empty;

        public List<Rentals> Rentals { get; private set; } = new List<Rentals>();

        public Customer(int id, string fullName, string driverLicenseNumber, string phone, string email) 
        {
            Id = id;
            FullName = fullName;
            DriverLicenseNumber = driverLicenseNumber;
            Phone = phone;
            Email = email;
        }

        public void UpdateContactInfo(string phone, string email) 
        {
            Phone = phone;
            Email = email;
        }

        public void AddRental(Rentals rental) 
        {
            Rentals.Add(rental);
        }
    }
}
