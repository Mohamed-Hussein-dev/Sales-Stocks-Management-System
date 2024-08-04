using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public Invoice Invoice { get; set; }

        [InverseProperty(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
