using System.Collections.Generic;
using LicenseService.Models;
using Microsoft.EntityFrameworkCore;

namespace LicenseService.Data
{
    public class SystemDatabase : DbContext
    {

        public SystemDatabase(DbContextOptions<SystemDatabase> options)
            : base(options)
        {

        }


        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerHistory> CustomerHistory { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Usage> Usage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Subscription>().ToTable("Subscription");
            modelBuilder.Entity<Usage>().ToTable("Usage");
            modelBuilder.Entity<CustomerHistory>().ToTable("CustomerHistory");
        }
    }


}
