using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSys.Domain.Entities;
using CarRentalSys.Domain.RentalObjects;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSys.Infrastructure.EFData
{
    public class AppDbContext : DbContext
    {

        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           if(!optionsBuilder.IsConfigured)
           {
                optionsBuilder.UseSqlServer("Server=DESKTOP-BLQDC9N;Database=CarRentals;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Rentals

            modelBuilder.Entity<Rentals>(builder =>
            {
                //foreignkeys
                builder.HasOne(r => r.Car).WithMany(c => c.Rentals).HasForeignKey(r => r.CarId);
                builder.HasOne(r => r.Customer).WithMany(c => c.Rentals).HasForeignKey(r => r.CustomerId);

                builder.Property(r => r.Status).HasConversion<string>().HasMaxLength(50).IsRequired();

                builder.Property(r => r.TotalPrice).HasPrecision(18, 2);
                builder.Property(r => r.Deposit).HasPrecision(18, 2);
            });

            modelBuilder.Entity<Inspections>(builder =>
            {

                builder.HasOne(i => i.Rentals).WithOne(r => r.Inspection).HasForeignKey<Inspections>(i => i.RentalID);
                builder.Property(i => i.TotalCost).HasPrecision(18, 2);
            });

            modelBuilder.Entity<Damage>(builder =>
            {
                builder.HasOne(d => d.Inspection).WithMany(i => i.Damages).HasForeignKey(d => d.InspectionId);
                builder.Property(d => d.DmgSeverity).HasConversion<string>().HasMaxLength(50);
                builder.Property(d => d.Description).HasMaxLength(100);
                builder.Property(d => d.damageCost).HasPrecision(18, 2);
            });

            modelBuilder.Entity<Payments>(builder =>
            {
                builder.HasOne(p => p.Rental).WithOne(r => r.Payment).HasForeignKey<Payments>(p => p.RentalId);
                builder.Property(p => p.CarCategory).HasConversion<string>().HasMaxLength(50);
                builder.Property(p => p.Amount).HasPrecision(18, 2);
            });

            modelBuilder.Entity<Customer>(builder =>
            {
                builder.Property(c => c.DriverLicenseNumber).HasMaxLength(50).IsRequired();
                builder.Property(c => c.Phone).HasMaxLength(20).IsRequired();

            });

            modelBuilder.Entity<Car>(builder =>
            {
                builder.Property(c => c.Category).HasConversion<string>().HasMaxLength(50);
                builder.Property(c => c.Status).HasConversion<string>().HasMaxLength(50);
                builder.Property(c => c.PricePerDay).HasPrecision(18, 2);
            });



        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Rentals> Rentals { get; set; }
        public DbSet<Damage> Damages { get; set; }
        public DbSet<Inspections> Inspections { get; set; }
        public DbSet<Payments> Payments { get; set; }


    }
}
