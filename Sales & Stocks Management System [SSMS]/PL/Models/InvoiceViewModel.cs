using DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Models
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public InvoiceStatus Status => (Balnced <= 0 ? InvoiceStatus.Paid : InvoiceStatus.Pending); //Pending , Paid
        public double Discount { get; set; }
        public double AmountPaid { get; set; }

        public double Balnced => (TotalAmount) - AmountPaid;
        public double TotalAmount { set; get; }
        public void CalcTotal(){
            if (InvoiceItems.Any())
                TotalAmount = InvoiceItems.Sum(item => item.Quantity*item.UnitPrice) - Discount;
        }
        public int ContactId { get; set; }
        public Contact ?Contact { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    }
}
