using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum InvoiceStatus {Pending, Paid}
    public abstract class Invoice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public InvoiceStatus Status { get; set; } //Pending , Paid
        public double Discount { get; set; }
        public double AmountPaid { get; set; }

        public double Balnced => TotalAmount - AmountPaid;
        public double TotalAmount { get; set; }
        public Contact Contact { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
    }
}
