using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Requrid")]
        [MinLength(3 , ErrorMessage = "Minimum Length is 3")]
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "Category Is Requrid")]
        [MinLength(3, ErrorMessage = "Minimum Length is 3")]
        public string Category { get; set; }

        [Required(ErrorMessage = "StockQuantity Is Requrid")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int StockQuantity { get; set; }
        public double LastPrice { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
        public ICollection<ProductPrice> ProductPrices { get; set; } = new List<ProductPrice>();
    }
}
