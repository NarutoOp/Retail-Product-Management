using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendorService.Models;

namespace VendorService.Data
{
    public class VendorContext: DbContext
    {
        public VendorContext(DbContextOptions opts): base(opts)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VendorStock>().HasData(
                   new VendorStock() { Id = 1, ProductId = 101, VendorId = 201, HandInStocks = 24, ReplinshmentDate = Convert.ToDateTime(" 2021 - 02 - 02") },
                   new VendorStock() { Id = 2, ProductId = 1, VendorId = 201, HandInStocks = 24, ReplinshmentDate = Convert.ToDateTime(" 2021 - 02 - 02") }
               );
            modelBuilder.Entity<Vendor>().HasData(
                  new Vendor() { Id = 201, Name = "DelhiMotoShop", DeliveryCharge = 45, Rating = 5 },
                  new Vendor() { Id = 202, Name = "HydMotoShop", DeliveryCharge = 50, Rating = 4 }
              );
        }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<VendorStock> VendorStock { get; set; }
    }
}
