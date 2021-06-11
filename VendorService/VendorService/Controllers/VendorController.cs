using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendorService.Models;
using VendorService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VendorService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        IVendorDetail<Vendor> _vendor;
        private readonly ILogger<VendorController> _logger;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(VendorController));
        public VendorController(IVendorDetail<Vendor> vendor,ILogger<VendorController> logger)
        {
            _vendor = vendor;
            _logger = logger;
        }
        
        // GET: api/<VendorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<VendorController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _log4net.Info("Getting Info");
            return Ok(_vendor.GetVendor(id));
        }

        // POST api/<VendorController>
        [HttpPost]
        public void Post([FromBody] VendorStock vs)
        {
            _vendor.PostStock(vs);
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
