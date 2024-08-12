using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGenaricRepository<T>
    {
        IQueryable<T> GetAll();
        T? GetById(int id);
        void Delete(T Entity);
        void Update(T Entity);
        void Add(T Entity);

    }
}
