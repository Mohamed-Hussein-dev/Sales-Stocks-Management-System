using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name can't exceed 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }
        public double TotalPendingInvoices => Invoices.Sum(In => In.TotalAmount  - In.AmountPaid);
        public ICollection<InvoiceViewModel> Invoices { get; set; } = new List<InvoiceViewModel>();
    }
}
