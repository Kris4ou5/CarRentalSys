using CarRentalSys.Application.Services;
using CarRentalSys.Domain.Entities;
using CarRentalSys.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSys.ConsoleUI
{
    public class RentalSystemUI
    {
        private readonly CarService _carService;
        private readonly CustomerService _customerService;
        private readonly RentalService _rentalService;

        public RentalSystemUI(CarService carService, CustomerService customerService, RentalService rentalService)
        {
            _carService = carService;
            _customerService = customerService;
            _rentalService = rentalService;
        }

        public void Start()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();

                Console.WriteLine("===== CAR RENTAL SYSTEM =====");
                Console.WriteLine("1.Add Car");
                Console.WriteLine("2. Show All Cars");
                Console.WriteLine("3. Register Customer");
                Console.WriteLine("4. Show Available Cars");
                Console.WriteLine("5. Customer Rental History");
                Console.WriteLine("0. Exit");

                Console.WriteLine("Choose option");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddCar();
                            break;

                        case "2":
                            ShowAllCars();
                            break;

                        case "3":
                            RegisterCustomer();
                            break;

                        case "4":
                            ShowAvailableCars();
                            break;

                        case "5":
                            ShowCustomerHistory();
                            break;

                        case "0":
                            running = false;
                            break;

                        default:
                            Console.WriteLine("Invalid option");
                            Pause();
                            break;

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                    Pause();
                }

                
            }
        }

        private void AddCar()
        {
            Console.Clear();

            Console.WriteLine("=== ADD CAR ===");

            Console.Write("Brand: ");
            string brand = Console.ReadLine();

            Console.Write("Model: ");
            string model = Console.ReadLine();

            Console.WriteLine("Category: ");
            foreach (var carCategory in Enum.GetValues(typeof(CarCategory)))
            {
                Console.WriteLine($"{(int)carCategory} - {carCategory}");
            }

            int categoryInput = int.Parse(Console.ReadLine());

            CarCategory category = (CarCategory)categoryInput;

            Console.Write("Price per day: ");
            decimal price = decimal.Parse(Console.ReadLine());

            _carService.AddCar(brand, model, category, price);

            Console.WriteLine("Car added successfully!");

            Pause();
        }

        private void ShowAllCars()
        {
            Console.Clear();

            Console.WriteLine("=== ALL CARS ===");

            var cars = _carService.GetAllCars();

            foreach (var car in cars)
            {
                Console.WriteLine(
                    $"ID: {car.Id} | " +
                    $"{car.Brand} {car.Model} | " +
                    $"{car.Category} | " +
                    $"{car.PricePerDay} lv/day | " +
                    $"Status: {car.Status}");
            }

            Pause();
        }

        private void RegisterCustomer()
        {
            Console.Clear();

            Console.WriteLine("=== REGISTER CUSTOMER ===");

            Console.Write("Full name: ");
            string name = Console.ReadLine();

            Console.Write("Driver license: ");
            string license = Console.ReadLine();

            Console.Write("Phone: ");
            string phone = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            _customerService.RegisterCustomer(name, license, phone, email);

            Console.WriteLine("Customer registered!");

            Pause();
        }

        private void ShowAvailableCars() 
        {
            Console.Clear();

            Console.WriteLine("=== AVAILABLE CARS ===");

            Console.Write("Start date (yyyy-mm-dd): ");
            DateTime start = DateTime.Parse(Console.ReadLine());

            Console.Write("End date (yyyy-mm-dd): ");
            DateTime end = DateTime.Parse(Console.ReadLine());

            var cars = _carService.GetAvailableCarsForPeriod(start, end);

            foreach (var car in cars)
            {
                Console.WriteLine(
                    $"ID: {car.Id} | " +
                    $"{car.Brand} {car.Model} | " +
                    $"{car.Category} | " +
                    $"{car.PricePerDay} lv/day");
            }

            Pause();
        }

        private void ShowCustomerHistory()
        {
            Console.Clear();

            Console.WriteLine("=== CUSTOMER HISTORY ===");

            Console.Write("Customer ID: ");

            int id = int.Parse(Console.ReadLine());

            var rentals = _customerService.GetCustomerRentalHistory(id);

            foreach (var rental in rentals)
            {
                Console.WriteLine(
                    $"Rental ID: {rental.Id} | " +
                    $"Car ID: {rental.CarId} | " +
                    $"{rental.StartDate:d} -> {rental.EndDate}");
            }

            Pause();
        }

        private void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}