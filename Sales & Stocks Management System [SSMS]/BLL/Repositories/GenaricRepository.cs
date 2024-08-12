using BLL.Interfaces;
using DAL.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public  class GenaricRepository<T> : IGenaricRepository<T> where T : class
    {
        private readonly Stock_SalseDbContext _dbContext;

        public GenaricRepository(Stock_SalseDbContext dbContext )
        {
            _dbContext = dbContext;
        }
        public void Add(T Entity)
        {
            _dbContext.Add(Entity);
        }
       
        public void Delete(T Entity)
        {
            _dbContext.Remove(Entity);
        }


        public virtual IQueryable<T> GetAll()
        {
           return _dbContext.Set<T>();
        }

        public virtual T? GetById(int id)
        {
            return _dbContext.Find<T>(id);
        }


        public void Update(T Entity)
        {
           _dbContext.Update(Entity);
        }
    }
}
