using System;
using System.Linq;

namespace kli.Blog.Core.Contracts.Data
{
    public interface IRepository<T> : IQueryable<T>
        where T : class
    {
        Lazy<int> Insert(T entity);
        Lazy<int> InsertOrUpdate(T entity);
        void Update(int id, object values);
        void Delete(T entity);
    }
}
