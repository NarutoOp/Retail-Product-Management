using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendorService.Data;
using VendorService.Models;

namespace VendorService.Repository
{
    public class VendorDetailRepo : IVendorDetailRepo<Vendor>
    {
        VendorContext _context;
        public VendorDetailRepo(VendorContext context)
        {
            _context = context;
        }
        public IEnumerable<Vendor> GetAll()
        {
            return _context.Vendor.ToList();
        }
        public IEnumerable<Vendor> GetVendor(int id)
        {
            try
            {
                IEnumerable<VendorStock> vs = _context.VendorStock.Include(v => v.Vendor).Where(v => v.ProductId == id && v.HandInStocks > 0);
                return vs.Select(v => v.Vendor);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public void PostStock(VendorStock vs)
        {
            vs.ReplinshmentDate = Convert.ToDateTime(vs.ReplinshmentDate);
            _context.VendorStock.Add(vs);
            _context.SaveChanges();
        }
    }
}