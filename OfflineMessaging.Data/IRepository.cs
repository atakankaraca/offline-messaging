using OfflineMessaging.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OfflineMessaging.Repository
{
   public interface IRepository<T, TKey>
        where T : class, IEntity<TKey>
        where TKey : IComparable
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByQuery(Expression<Func<T, bool>> query);
        T Get(TKey key);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void SaveChanges();
    }

}
