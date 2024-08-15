using BLL.Interfaces;
using DAL.DbContexts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class ProductRepository : GenaricRepository<Product> , IProduct
    {
        public ProductRepository(Stock_SalseDbContext dbContext) : base(dbContext){ }

        public override Product? GetById(int id)
        {
            return Entities.Include(p => p.ProductPrices).FirstOrDefault(p => p.Id == id);
        }

        public override IQueryable<Product> GetAll()
        {
            return Entities.Include(product => product.ProductPrices);
        }
    }
}
