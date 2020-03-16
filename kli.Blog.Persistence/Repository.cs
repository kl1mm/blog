using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using kli.Blog.Core.Contracts.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace kli.Blog.Persistence
{
    internal class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly DataContext context;
        private readonly IQueryable<T> queryable;

        internal Repository(DataContext context)
        {
            this.context = context;
            this.queryable = this.context.Set<T>().AsQueryable();
        }

        Lazy<int> IRepository<T>.Insert(T entity) => this.IdAccessor(this.context.Attach(entity));

        Lazy<int> IRepository<T>.InsertOrUpdate(T entity) => this.IdAccessor(this.context.Update(entity));

        void IRepository<T>.Delete(T entity) => this.context.Remove(entity);

        void IRepository<T>.Update(int id, object values)
        {
            var existingEntity = this.context.Find<T>(id);
            if (existingEntity == null)
                throw new KeyNotFoundException($"Key '{id}' in table '{typeof(T).Name}' not found.");

            this.context.Entry(existingEntity).CurrentValues.SetValues(values);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => this.queryable.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.queryable.GetEnumerator();

        Type IQueryable.ElementType => this.queryable.ElementType;

        Expression IQueryable.Expression => this.queryable.Expression;

        IQueryProvider IQueryable.Provider => this.queryable.Provider;

        private Lazy<int> IdAccessor(EntityEntry<T> entry)
        {
            return new Lazy<int>(() =>
            {
                if (entry.State != EntityState.Unchanged)
                    throw new InvalidOperationException("Access to Primarykey not possible before entity is saved (Scope must be completed)");
                return entry.CurrentValues.GetValue<int>("Id");
            });
        }
    }
}
