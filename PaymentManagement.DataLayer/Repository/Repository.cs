using Microsoft.EntityFrameworkCore;
using PaymentManagement.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PaymentManagement.DataLayer.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Delete(T entity)
        {
            _applicationDbContext.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _applicationDbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _applicationDbContext.Set<T>().Find(id);
        }

        public void Insert(T entity)
        {
            _applicationDbContext.Set<T>().Add(entity);
        }

        public IEnumerable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return _applicationDbContext.Set<T>().Where(predicate);
        }

        public void Update(T entity)
        {
            _applicationDbContext.Entry(entity).State = EntityState.Modified;
            _applicationDbContext.Set<T>().Attach(entity);
        }
    }
}
