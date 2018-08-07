using OfflineMessaging.Data;
using System;
using System.Linq;

namespace OfflineMessaging.Repository
{
   public interface IRepository<T, TKey>
        where T : class, IEntity<TKey>
        where TKey : IComparable
    {
        IQueryable<T> GetAll();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void SaveChanges();
    }

}
