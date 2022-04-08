using System;
using System.Data.Entity;
using System.Linq;

namespace ProjectManager.Models.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity, new()
    {
        protected readonly DbContext context;
        public Repository()
        {
            this.context = App.DbContext;
        }

        public virtual IQueryable<T> Items => context.Set<T>();

        public T Add(T entity)
        {
            if (entity.Equals(null))
            {
                throw new ArgumentNullException(nameof(entity));
            }
            T result = context.Set<T>().Add(entity);
            context.SaveChanges();
            return result;
        }

        public T Get(int id)
        {
            return Items.SingleOrDefault(entity => entity.Id == id);
        }

        public void Remove(int id)
        {
            T entity = Get(id) ?? new T() { Id = id };
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity.Equals(null))
            {
                throw new ArgumentNullException(nameof(entity));
            }
            context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
