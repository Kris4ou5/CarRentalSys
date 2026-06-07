using CarRentalSys.Application.Services;
using CarRentalSys.Domain.Entities;
using CarRentalSys.Domain.Enums;
using CarRentalSys.Domain.RentalObjects;
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
                Console.WriteLine("1. Register Customer");
                Console.WriteLine("2. Update Customer Contact");
                Console.WriteLine("3. AddCar");
                Console.WriteLine("4. Show All Cars");
                Console.WriteLine("5. Show Available Cars");
                Console.WriteLine("6. Create Rental");
                Console.WriteLine("7. Cancel Rental");
                Console.WriteLine("8. Start Rental");
                Console.WriteLine("9. Complete Rental");
                Console.WriteLine("10. Register Inspection");
                Console.WriteLine("11. Process Payment");
                Console.WriteLine("12. Return Deposit");
                Console.WriteLine("13. Send Car To Maintenance");
                Console.WriteLine("14. Return Car From Maintenance");
                Console.WriteLine("15. Show Active Rentals");
                Console.WriteLine("16. Show Customer Rental History");
                Console.WriteLine("17. Show Fleet Usage Report");
                Console.WriteLine("18. Show Revenue Report");
                Console.WriteLine("0. Exit");

                Console.WriteLine("Choose option");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            RegisterCustomer();
                            break;

                        case "2":
                            UpdateCustomerContact();
                            break;

                        case "3":
                            AddCar();
                            break;

                        case "4":
                            ShowAllCars();
                            break;

                        case "5":
                            ShowAvailableCars();
                            break;

                        case "6":
                            CreateRental();
                            break;

                        case "7":
                            CancelRental();
                            break;

                        case "8":
                            StartRental();
                            break;

                        case "9":
                            CompleteRental();
                            break;

                        case "10":
                            RegisterInspection();
                            break;

                        case "11":
                            ProcessPayment();
                            break;

                        case "12":
                            ReturnDeposit();
                            break;

                        case "13":
                            SendCarToMaintenance();
                            break;

                        case "14":
                            ReturnCarFromMaintenance();
                            break;

                        case "15":
                            ShowActiveRentals();
                            break;

                        case "16":
                            ShowCustomerHistory();
                            break;

                        case "17":
                            FleetUsageReport();
                            break;

                        case "18":
                            RevenueReport();
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

        private void UpdateCustomerContact()
        {
            Console.Clear();

            Console.WriteLine("=== UPDATE CUSTOMER ===");

            Console.Write("Customer ID: ");
            int id = int.Parse(Console.ReadLine());

            string fullname = "";
            string driverlicense = "";

            Console.Write("New phone: ");
            string phone = Console.ReadLine();

            Console.Write("New email: ");
            string email = Console.ReadLine();



            _customerService.UpdateCustomerContact(
                id,
                fullname,
                driverlicense,
                phone,
                email);

            Console.WriteLine("Customer updated!");

            Pause();
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

        public void CreateRental()
        {
            Console.Clear();

            Console.WriteLine("=== CREATE RENTAL ===");

            Console.Write("Customer ID: ");
            int customerId = int.Parse(Console.ReadLine());

            Console.Write("Car ID: ");
            int carId = int.Parse(Console.ReadLine());

            Console.Write("Start date: ");
            DateTime start = DateTime.Parse(Console.ReadLine());

            Console.Write("End date: ");
            DateTime end = DateTime.Parse(Console.ReadLine());

            Console.Write("Promo (%): ");
            int promo = int.Parse(Console.ReadLine());

            var rental = _rentalService.CreateRental(
                customerId,
                carId,
                start,
                end,
                promo);

            Console.WriteLine("Rental created!");
            Console.WriteLine($"Total: {rental.TotalPrice}");
            Console.WriteLine($"Deposit: {rental.Deposit}");

            Pause();
        }

        private void CancelRental()
        {
            Console.Clear();

            Console.Write("Rental ID: ");

            int id = int.Parse(Console.ReadLine());

            _rentalService.CancelRental(id);

            Console.WriteLine("Rental cancelled!");

            Pause();

        }

        private void StartRental()
        {
            Console.Clear();

            Console.Write("Rental ID: ");

            int id = int.Parse(Console.ReadLine());

            _rentalService.StartRental(id);

            Console.WriteLine("Rental started!");

            Pause();
        }

        private void CompleteRental()
        {
            Console.Clear();

            Console.Write("Rental ID: ");

            int id = int.Parse(Console.ReadLine());

            _rentalService.CompleteRental(id);

            Console.WriteLine("Rental completed!");

            Pause();
        }

        private void RegisterInspection()
        {
            Console.Clear();

            Console.WriteLine("=== REGISTER INSPECTION ===");

            Console.Write("Rental ID: ");

            int rentalId =
                int.Parse(Console.ReadLine());

            List<Damage> damages =
                new List<Damage>();

            Console.Write("Damage count: ");

            int count =
                int.Parse(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                Console.Write("Description: ");

                string desc =
                    Console.ReadLine();

                Console.WriteLine("Severity:");
                Console.WriteLine("0 - Minor");
                Console.WriteLine("1 - Moderate");
                Console.WriteLine("2 - Severe");

                DamageSeverity severity =
                    (DamageSeverity)int.Parse(Console.ReadLine());

                damages.Add(
                    new Damage(desc, severity));
            }

            var inspection =
                _rentalService.RegisterInspection(
                    rentalId,
                    damages);

            Console.WriteLine(
                $"Damage cost: {inspection.TotalCost}");

            Pause();
        }

        private void ProcessPayment()
        {
            Console.Clear();

            Console.WriteLine("=== PROCESS PAYMENT ===");

            Console.Write("Rental ID: ");

            int rentalId =
                int.Parse(Console.ReadLine());

            var payment =
                _rentalService.ProcessPayment(rentalId);

            Console.WriteLine(
                $"Payment processed: {payment.Amount} lv");

            Pause();
        }

        private void ReturnDeposit()
        {
            Console.Clear();

            Console.WriteLine("=== RETURN DEPOSIT ===");

            Console.Write("Rental ID: ");

            int rentalId =
                int.Parse(Console.ReadLine());

            decimal amount =
                _rentalService.ReturnDeposit(rentalId);

            Console.WriteLine(
                $"Returned deposit: {amount} lv");

            Pause();
        }

        private void SendCarToMaintenance()
        {
            Console.Clear();

            Console.WriteLine("=== SEND CAR TO MAINTENANCE ===");

            Console.Write("Car ID: ");

            int carId = int.Parse(Console.ReadLine());

            _carService.SendToMaintenance(carId);

            Console.WriteLine("Car sent to maintenance!");

            Pause();
        }

        private void ReturnCarFromMaintenance()
        {
            Console.Clear();

            Console.WriteLine("=== RETURN CAR FROM MAINTENANCE ===");

            Console.Write("Car ID: ");

            int carId = int.Parse(Console.ReadLine());

            _carService.ReturnFromMaintenance(carId);

            Console.WriteLine("Car returned from maintenance!");

            Pause();
        }

        private void ShowActiveRentals()
        {
            Console.Clear();

            Console.WriteLine("=== ACTIVE RENTALS ===");

            var rentals = _rentalService.GetActiveRentals();

            foreach (var rental in rentals)
            {
                Console.WriteLine(
                    $"Rental ID: {rental.Id} | " +
                    $"Customer: {rental.CustomerId} | " +
                    $"Car: {rental.CarId} | " +
                    $"{rental.StartDate:d} -> {rental.EndDate:d}");
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

        private void FleetUsageReport()
        {
            Console.Clear();

            Console.WriteLine("=== FLEET REPORT ===");

            double usage = _rentalService.FleetUsageReport();

            Console.WriteLine($"Fleet usage: {usage:F2}%");

            Pause();
        }

        private void RevenueReport()
        {
            Console.Clear();

            Console.WriteLine("=== REVENUE REPORT ===");

            Console.Write("Start date: ");

            DateTime start =
                DateTime.Parse(Console.ReadLine());

            Console.Write("End date: ");

            DateTime end =
                DateTime.Parse(Console.ReadLine());

            decimal revenue =
                _rentalService.GetRevenueForPeriod(start, end);

            Console.WriteLine($"Revenue: {revenue} lv");

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