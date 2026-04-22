using CarRentalSys.Application.Interfaces;
using CarRentalSys.Application.Services;
using CarRentalSys.Infrastructure;

namespace CarRentalSys
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileStorage storage = new FileStorage("rentals.json");

            ICustomerRepository customerRepo = new FileCustomersRepository(storage);

            IRentalRepository rentalRepo = new FileRentalsRepository(storage);

        }
    }
}
