using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tesodev.Models
{
    public class Address
    {
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int CityCode { get; set; }
    }
}
