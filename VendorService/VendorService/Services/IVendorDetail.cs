using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendorService.Models;

namespace VendorService.Services
{
    public interface IVendorDetail<T>
    {
        public IEnumerable<Vendor> GetVendor(int id);
    }
}
