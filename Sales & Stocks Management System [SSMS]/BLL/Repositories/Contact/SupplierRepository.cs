using BLL.Interfaces.Contact;
using DAL.DbContexts;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories.Contact
{
    public class SupplierRepository : GenaricRepository<Supplier>, ISupplier
    {
        private readonly Stock_SalseDbContext _dbContext;

        public SupplierRepository(Stock_SalseDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Supplier> GetByName(string name)
        {
            return _dbContext.Suppliers.Where(x => x.Name.Contains(name)).ToList();
        }
    }
}
