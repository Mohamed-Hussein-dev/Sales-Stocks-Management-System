using BLL.Interfaces.Invoice;
using DAL.DbContexts;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories.Invoice
{
    public class SalesInvoiceRepository : GenaricRepository<SaleInvoice> , ISalesInvoice
    {
        public SalesInvoiceRepository(Stock_SalseDbContext dbContext) : base(dbContext){}

        public double GetTotal(SaleInvoice Invoice)
        {
            double total = Invoice.InvoiceItems.Sum(x => x.Quantity * x.UnitPrice);
            double netTotal = total - (total * Invoice.Discount);
            return netTotal;
        }

       
    }
}
