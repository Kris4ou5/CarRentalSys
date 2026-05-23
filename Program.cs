using CarRentalSys.Application.Interfaces;
using CarRentalSys.Application.Services;
using CarRentalSys.ConsoleUI;
using CarRentalSys.Infrastructure;
using CarRentalSys.Infrastructure.EFData;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSys
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
       .UseSqlServer("Server=DESKTOP-BLQDC9N;Database=CarRentals;Trusted_Connection=True;TrustServerCertificate=True;")
       .Options;

            using var context = new AppDbContext(options);

            ICarRepository carRepo = new EFCarsRepository(context);

            ICustomerRepository customerRepo = new EFCustomersRepository(context);

            IRentalRepository rentalRepo = new EFRentalsRepository(context);

            CarService carService = new CarService(carRepo, rentalRepo);

            CustomerService customerService = new CustomerService(customerRepo, rentalRepo);

            RentalService rentalService = new RentalService(carRepo, rentalRepo, customerRepo);

            RentalSystemUI ui = new RentalSystemUI(carService, customerService, rentalService);

            ui.Start();

        }
    }
}
