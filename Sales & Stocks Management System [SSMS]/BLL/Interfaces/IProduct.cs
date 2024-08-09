using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProduct : IGenaricRepository<Product>
    {
        IQueryable<Product> GetByName(string name);
        IQueryable<Product> GetByStock(int mn , int mx = int.MaxValue);
        IQueryable<Product> GetByCategory(string Category);

    }
}
