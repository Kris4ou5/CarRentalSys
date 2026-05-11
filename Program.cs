using CarRentalSys.Application.Interfaces;
using CarRentalSys.Application.Services;
using CarRentalSys.ConsoleUI;
using CarRentalSys.Infrastructure;

namespace CarRentalSys
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileStorage storage = new FileStorage("rentals.json");

            ICarRepository carRepo = new FileCarsRepository(storage);

            ICustomerRepository customerRepo = new FileCustomersRepository(storage);

            IRentalRepository rentalRepo = new FileRentalsRepository(storage);

            CarService carService = new CarService(carRepo, rentalRepo);

            CustomerService customerService = new CustomerService(customerRepo, rentalRepo);

            RentalService rentalService = new RentalService(carRepo, rentalRepo, customerRepo);

            RentalSystemUI ui = new RentalSystemUI(carService, customerService, rentalService);

            ui.Start();

        }
    }
}
