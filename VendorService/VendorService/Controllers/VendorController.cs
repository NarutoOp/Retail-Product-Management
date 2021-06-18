using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendorService.Models;
using VendorService.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VendorService.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        IVendorDetailRepo<Vendor> _vendor;
        private readonly ILogger<VendorController> _logger;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(VendorController));
        public VendorController(IVendorDetailRepo<Vendor> vendor,ILogger<VendorController> logger)
        {
            _vendor = vendor;
            _logger = logger;
        }
        
        // GET: api/<VendorController>
        [HttpGet]
        public IEnumerable<Vendor> Get()
        {
            return _vendor.GetAll();
        }

        // GET api/<VendorController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _log4net.Info("Getting Info");
            List<Vendor> vendors = _vendor.GetVendor(id).ToList();
            if(vendors.Count() == 0)
            {
                return NotFound("No Vendors Found");
            }
            return Ok(vendors);
        }

        // POST api/<VendorController>
        [HttpPost]
        public void Post([FromBody] VendorStock vs)
        {
            _vendor.PostStock(vs);
        }

    }
}
