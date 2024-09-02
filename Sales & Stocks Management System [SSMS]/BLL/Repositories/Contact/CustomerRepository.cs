using BLL.Interfaces.Contact;
using DAL.DbContexts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories.Contact
{
    public class CustomerRepository : GenaricRepository<Customer> , ICustomer
    {
        private readonly Stock_SalseDbContext _dbContext;

        public CustomerRepository(Stock_SalseDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //public IQueryable<Customer> Entity => _dbContext.Customers;

        public override IQueryable<Customer> GetAll()
        {
            return Entities.Include(Cus => Cus.Invoices);
        }
        public IEnumerable<Customer> GetByName(string name)
        {
            return _dbContext.Customers.Where(x => x.Name.Contains(name)).ToList();
        }
    }
}
