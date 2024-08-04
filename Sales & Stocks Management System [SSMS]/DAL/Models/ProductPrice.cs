using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ProductPrice
    {
        public int Id { get; set; }
        public double Price { get; set; }

        public Product Product { get; set; }
    }
}
