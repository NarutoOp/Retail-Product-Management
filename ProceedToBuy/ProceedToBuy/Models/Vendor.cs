using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuy.Models
{
    public class VendorStock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int DeliveryCharge { get; set; }
        public int Rating { get; set; }

    }
}
