using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Models
{
    public class UserCredentials
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime BanTime { get; set; } = Convert.ToDateTime(" 2000 - 02 - 02");
        [Range(0, 5)]
        public int Counter { get; set; } = 0;
        public string Address { get; set; }
    }
}
