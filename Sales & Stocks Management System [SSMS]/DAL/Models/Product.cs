using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ?Description { get; set; }
        public string Category { get; set; }
        public int StockQuantity { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
        public ICollection<ProductPrice> ProductPrices { get; set; } = new List<ProductPrice>();
    }
}
