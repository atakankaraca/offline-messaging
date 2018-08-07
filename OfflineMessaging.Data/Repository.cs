using Microsoft.EntityFrameworkCore;
using OfflineMessaging.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OfflineMessaging.Repository
{
    public class Repository<T, TKey> : IRepository<T, TKey>
        where T : class, IEntity<TKey>
        where TKey: IComparable
    {
        private readonly ApplicationContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(ApplicationContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public IEnumerable<T> GetByQuery(Expression<Func<T, bool>> query)
        {
            return entities.Where(query);
        }

        public T Get(TKey key)
        {
            return entities.Find(key);
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entity.CreatedDate = DateTime.UtcNow;
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
