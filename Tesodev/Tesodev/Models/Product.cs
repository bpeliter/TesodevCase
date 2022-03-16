using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tesodev.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }

    }
}
