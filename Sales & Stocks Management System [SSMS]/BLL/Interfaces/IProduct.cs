using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProduct : IGenaricRepository<Product>
    {
       public DbSet<Product> Entity { get; }
    }
}
