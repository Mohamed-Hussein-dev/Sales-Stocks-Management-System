using BLL.Interfaces;
using DAL.DbContexts;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class ProductRepository : GenaricRepository<Product> , IProduct
    {
        private readonly Stock_SalseDbContext dbContext;

        public ProductRepository(Stock_SalseDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Product> GetByCategory(string Category)
        {
            return dbContext.Products.Where(product => product.Category.Contains(Category));
        }

        public IQueryable<Product> GetByName(string name)
        {
            return dbContext.Products.Where(product => product.Name.Contains(name));
        }

        public IQueryable<Product> GetByStock(int mn, int mx = int.MaxValue)
        {
            return dbContext.Products.Where(product => product.StockQuantity >= mn && product.StockQuantity <= mx);
        }
    }
}
