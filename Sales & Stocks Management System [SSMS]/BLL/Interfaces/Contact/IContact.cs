using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Contact
{
    public interface IContact<T> : IGenaricRepository<T> where T : class
    {
        public IEnumerable<T> GetByName(string name);
       
    }
}
