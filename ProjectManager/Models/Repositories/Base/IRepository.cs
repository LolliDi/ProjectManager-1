using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Models.Repositories
{
    public interface IRepository<T> where T : class, IEntity
    {
        IQueryable<T> Items { get; }
        T Get(int id);
        T Add(T entity);
        void Remove(int id);
        void Update(T entity);
    }
}
