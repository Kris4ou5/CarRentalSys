using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSys.Application.Interfaces;
using CarRentalSys.Domain.Entities;
using CarRentalSys.Domain.Enums;
using CarRentalSys.Domain.RentalObjects;

namespace CarRentalSys.Application.Services
{
    public class RentalService
    {
        private readonly ICarRepository _carRepo;
        private readonly IRentalRepository _rentalRepo;
        private readonly ICustomerRepository _customerRepo;

        public RentalService(ICarRepository carRepo, IRentalRepository rentalRepo, ICustomerRepository customerRepo)
        {
            _carRepo = carRepo;
            _rentalRepo = rentalRepo;
            _customerRepo = customerRepo;
        }

        public decimal Price(Car car, int days, int promo)
        {
            decimal discount = promo / 100m;
            decimal categoryMulti = (decimal)car.Category / 10 + 1;
            decimal price = car.PricePerDay * days * categoryMulti;
            price -= price * discount;
            return price;
        }

        public Rentals CreateRental(int customerId, int carId, DateTime startDate, DateTime endDate, int promo)
        {
            var customer = _customerRepo.GetById(customerId);

            if (customer == null)
                throw new Exception("Customer not found");

            var car = _carRepo.GetById(carId);

            if (car == null)
                throw new Exception("Car not found");

            if (car.Status != CarStatus.Available)
                throw new Exception("Car is not available");

            var rentals = _rentalRepo.GetAll();

            foreach (var rental in rentals)
            {
                if (rental.CarId == carId)
                {
                    bool overlap = rental.StartDate <= endDate && rental.EndDate >= startDate;

                    if (overlap)
                        throw new Exception("Car already rented for this period");
                }

            }

            Rentals newRental = new Rentals(carId, customerId, startDate, endDate);

            int days = newRental.GetTotalDays();

            decimal totalPrice = Price(car, days, promo);

            newRental.SetPrice(totalPrice);

            decimal deposit = totalPrice * 0.20m;

            newRental.SetDeposit(deposit);

            _rentalRepo.Save(newRental);

            return newRental;
        }

        public void CancelRental(int rentalId)
        {
            var rental = _rentalRepo.GetById(rentalId);

            if (rental == null)
                throw new Exception("Rental not found");

            if (DateTime.Now >= rental.StartDate)
                throw new Exception("Rental already started");

            int daysBeforeStart = (rental.StartDate - DateTime.Now).Days;

            if (daysBeforeStart < 3)
            {
                decimal penalty =
                    rental.TotalPrice * 0.10m;

                rental.SetPrice(
                    rental.TotalPrice + penalty);
            }

            rental.ChangeStatus(RentalStatus.Canncelled);

            _rentalRepo.Save(rental);
        }

        public void StartRental(int rentalId)
        {
            var rental = _rentalRepo.GetById(rentalId);

            if (rental == null)
                throw new Exception("Rental not found");

            var car = _carRepo.GetById(rental.CarId);

            rental.ChangeStatus(RentalStatus.Active);

            car.ChangeStatus(CarStatus.Rented);

            _rentalRepo.Save(rental);

            _carRepo.Save(car);
        }

        public void CompleteRental(int rentalId)
        {
            var rental = _rentalRepo.GetById(rentalId);

            if (rental == null)
                throw new Exception("Rental not found");

            var car = _carRepo.GetById(rental.CarId);

            rental.ChangeStatus(RentalStatus.Completed);

            car.ChangeStatus(CarStatus.Available);

            _rentalRepo.Save(rental);

            _carRepo.Save(car);
        }

        public Inspections RegisterInspection(int rentalId, List<Damage> damages)
        {
            var rental = _rentalRepo.GetById(rentalId);

            if (rental == null)
                throw new Exception("Rental not found");

            Inspections inspection = new Inspections(0, rentalId, damages);

            inspection.CalculateDamagesCost();

            rental.AddInspection(inspection);

            decimal finalPrice = rental.TotalPrice + inspection.TotalCost;

            rental.SetPrice(finalPrice);

            _rentalRepo.Save(rental);

            return inspection;
        }

        public Payments ProcessPayment(int rentalId)
        {
            var rental = _rentalRepo.GetById(rentalId);

            if (rental == null)
                throw new Exception("Rental not found");

            var car = _carRepo.GetById(rental.CarId);

            Payments payment = new Payments(rentalId, car.Category, (int)rental.TotalPrice);

            rental.AddPayment(payment);

            _rentalRepo.Save(rental);

            return payment;
        }

        public decimal ReturnDeposit(int rentalId)
        {
            var rental = _rentalRepo.GetById(rentalId);

            if (rental == null)
                throw new Exception("Rental not found");

            bool hasDamages = rental.Inspection != null && rental.Inspection.TotalCost > 0;

            if (hasDamages)
                return 0;

            return rental.Deposit;
        }

        public IReadOnlyList<Rentals> GetActiveRentals()
        {
            var rentals = _rentalRepo.GetAll();

            return rentals
                .Where(r => r.Status == RentalStatus.Active)
                .ToList();
        }

        public double FleetUsageReport()
        {
            var cars = _carRepo.GetAll();

            if (cars.Count == 0)
                return 0;

            int rentedCars = cars.Count(c => c.Status == CarStatus.Rented);

            return (double)rentedCars / cars.Count * 100;
        }

        public decimal GetRevenueForPeriod(DateTime start, DateTime end)
        {
            var rentals = _rentalRepo.GetAll();

            decimal revenue = 0;

            foreach (var rental in rentals)
            {
                if (rental.StartDate >= start &&
                    rental.EndDate <= end)
                {
                    revenue += rental.TotalPrice;
                }
            }

            return revenue;
        }

        public IReadOnlyList<Rentals> GetAllRentals()
        {
            return _rentalRepo.GetAll();
        }
    }
}
