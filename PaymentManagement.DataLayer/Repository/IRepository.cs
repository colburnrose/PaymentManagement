using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace PaymentManagement.DataLayer.Repository
{
    public interface IRepository<T> where T : class
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        T GetById(int id);
    }

    public abstract class Entity
    {
        public int Id { get; protected set; }
    }
}
