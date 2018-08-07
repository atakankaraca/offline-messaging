using Microsoft.EntityFrameworkCore;
using OfflineMessaging.Data;
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

        public IQueryable<T> GetAll()
        {
            return entities.AsQueryable();
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> query)
        {
            return entities.Where(query);
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
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
