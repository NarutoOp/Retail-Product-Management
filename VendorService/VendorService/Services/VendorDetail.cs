using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendorService.Models;

namespace VendorService.Services
{
    public class VendorDetail:IVendorDetail<Vendor>
    {
        VendorContext _context;
        public VendorDetail(VendorContext context)
        {
            _context = context; 
        }
        public IEnumerable<Vendor> GetVendor(int id)
        {
            IEnumerable < VendorStock > vs = _context.VendorStock.Include(v => v.Vendor).Where(v => v.ProductId == id && v.HandInStocks>0);
            return vs.Select(v=>v.Vendor);
        }
        public void PostStock(VendorStock vs)
        {
            vs.ReplinshmentDate = Convert.ToDateTime(vs.ReplinshmentDate);
            _context.VendorStock.Add(vs);
            _context.SaveChanges();
        }
    }
}
