using BLL.Interfaces;
using BLL.Interfaces.Contact;
using BLL.Interfaces.Invoice;
using BLL.Repositories.Contact;
using DAL.DbContexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class UnitOfWork : IUnitOfWrok , IDisposable
    {
        private readonly Stock_SalseDbContext _dbContext;
        private readonly IServiceProvider _serviceProvider;

        private ISalesInvoice _salesInvoiceRepo;
        private IPurchaseInvoice _purchaseInvoiceRepo;
        private IProduct _productRepo;
        private ICustomer _customerRepo;
        private ISupplier _supplierRepo;
        private IInvioceItem _invioceItemRepo;
        public UnitOfWork(Stock_SalseDbContext dbContext , IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
        }

        public ISalesInvoice SalesInvoiceRepository => _salesInvoiceRepo ??= _serviceProvider.GetRequiredService<ISalesInvoice>();
        public IPurchaseInvoice PurchaseInvoiceRepository => _purchaseInvoiceRepo ??= _serviceProvider.GetRequiredService<IPurchaseInvoice>();
        public IProduct ProductRepository => _productRepo ??= _serviceProvider.GetRequiredService<IProduct>();
        public ICustomer CustomerRepository => _customerRepo ??= _serviceProvider.GetRequiredService<ICustomer>();
        public ISupplier SupplierRepository => _supplierRepo ??= _serviceProvider.GetRequiredService<ISupplier>();
        public IInvioceItem InvioceItemRepository => _invioceItemRepo ??= _serviceProvider.GetRequiredService<IInvioceItem>();

        public int Save()
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
