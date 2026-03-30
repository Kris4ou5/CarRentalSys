using CarRentalSys.Domain.Enums;
using CarRentalSys.Domain.RentalObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSys.Domain.Entities
{
    public class Rentals
    {
        public int Id { get; private set; }
        public int CustomerId { get; private set; }
        public int CarId { get; private set; }   

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public RentalStatus Status { get; private set; }

        public decimal TotalPrice { get; private set; }
        public decimal Deposit { get; private set; }

        public Inspections? Inspection { get; private set; }
        public Payments? Payment { get; private set; }

        public Rentals(int id,int carId,int customerId, DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
                throw new ArgumentException("End date cannot be before start date");
            if(id <= 0 || customerId <=0 || carId<=0)
                throw new ArgumentException("ID cannot be negative");
            Id = id; 
            CustomerId = customerId;
            CarId = carId;

            StartDate = startDate;
            EndDate = endDate;

            Status = RentalStatus.Pending;
        }

        public int GetTotalDays()
        {
            return (EndDate - StartDate).Days+1;
        }

        public void SetPrice(decimal price)
        {
            if (price < 0)
                throw new ArgumentException("Price cannot be negative");
            TotalPrice = price;
        }

        public void SetDeposit(decimal deposit)
        {
            if (deposit < 0)
                throw new ArgumentException("Deposit cannot be negative");
            Deposit = deposit;
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
