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
    public class InvoiceItemRepository : GenaricRepository<InvoiceItem>, IInvioceItem
    {
        public InvoiceItemRepository(Stock_SalseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
