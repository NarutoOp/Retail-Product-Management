using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendorService.Models;
using VendorService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VendorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        IVendorDetail<Vendor> _vendor;
        public VendorController(IVendorDetail<Vendor> vendor)
        {
            _vendor = vendor;
        }
        
        // GET: api/<VendorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<VendorController>/5
        [HttpGet("{id}")]
        public IEnumerable<Vendor> Get(int id)
        {

            return _vendor.GetVendor(id);
        }

        // POST api/<VendorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<VendorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VendorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
