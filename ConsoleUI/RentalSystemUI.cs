using CarRentalSys.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSys.ConsoleUI
{
    internal class RentalSystemUI
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
            }
        }
    }
}