using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Portal.Models
{
    public class UserToken
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Address { get; set; }
    }
}
