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
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<VendorStock> VendorStock { get; set; }
    }
}
