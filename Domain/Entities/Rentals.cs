using CarRentalSys.Domain.Enums;
using CarRentalSys.Domain.RentalObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSys.Domain.Entities
{
    public class Rentals
    {
        public int Id { get; private set; }

        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; }

        public int CarId { get; private set; }
        public Car Car { get; private set; }

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public RentalStatus Status { get; private set; }

        public decimal TotalPrice { get; private set; }
        public decimal Deposit { get; private set; }

        public Inspections? Inspection { get; private set; }
        public Payments? Payment { get; private set; }

        public Rentals(int id, Customer customer, Car car, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Customer = customer;
            CustomerId = customer.Id;

            Car = car;
            CarId = car.Id;

            StartDate = startDate;
            EndDate = endDate;

            Status = RentalStatus.Pending;
        }

        public int GetTotalDays()
        {
            return (EndDate - StartDate).Days;
        }

        public void SetPrice(decimal price)
        {
            TotalPrice = price;
        }

        public void SetDeposit(decimal deposit)
        {
            Deposit = deposit;
        }

        public void StartRental()
        {
            Status = RentalStatus.Active;
            Car.ChangeStatus(CarStatus.Rented);
        }

        public void CompleteRental()
        {
            Status = RentalStatus.Completed;
            Car.ChangeStatus(CarStatus.Available);
        }

        public void CancelRental()
        {
            Status = RentalStatus.Canncelled;
        }

        public void AddInspection(Inspections inspection)
        {
            Inspection = inspection;
        }

        public void AddPayment(Payments payment)
        {
            Payment = payment;
        }
     
    }
}
