using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace CarRentalSys.Domain.Entities
{
    public class Customer
    {
        public int Id { get; private set; }
        public string FullName { get; private set; }
        public string DriverLicenseNumber { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; } = string.Empty;

        public  ICollection<Rentals> Rentals { get; private set; } = new List<Rentals>();

        public Customer() { }

        public Customer(string fullName, string driverLicenseNumber, string phone, string email) 
        {
            if (fullName.IsNullOrEmpty()) throw new ArgumentNullException("You must set name");
            if (driverLicenseNumber.IsNullOrEmpty()) throw new ArgumentNullException("You must set drivers license");
            if (phone.IsNullOrEmpty()) throw new ArgumentNullException("You must set a phone");

            FullName = fullName;
            DriverLicenseNumber = driverLicenseNumber;
            Phone = phone;
            Email = email;
        }

        public void UpdateContactInfo(string fullName, string driverLicenseNumber, string phone, string email) 
        {
            FullName = fullName;
            DriverLicenseNumber = driverLicenseNumber;
            Phone = phone;
            Email = email;
        }

        public void AddRental(Rentals rental) 
        {
            Rentals.Add(rental);
        }
    }
}
