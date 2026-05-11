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

        }

        private void ShowAllCars()
        {

        }

        private void RegisterCustomer()
        {

        }

        private void ShowAvailableCars() 
        {
        
        }

        private void ShowCustomerHistory()
        {

        }

        private void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key...");
            Console.WriteLine();
        }
    }
}