using Microsoft.EntityFrameworkCore;
using kli.Blog.Core.Contracts.Data;
using System.Threading.Tasks;
using kli.Blog.Persistence;

namespace kli.Blog.Core
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;
        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

        IUnitOfWorkScope IUnitOfWork.Begin() => new UnitOfWorkScope(this.context);
        IUnitOfWorkWriteScope IUnitOfWork.BeginWrite() => new UnitOfWorkWriteScope(this.context);

        private class UnitOfWorkScope : IUnitOfWorkScope
        {
            protected readonly DataContext context;

            public IRepository<T> SetOf<T>() where T : class => new Repository<T>(this.context);

            public UnitOfWorkScope(DataContext context)
            {
                this.context = context;
                this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            public virtual void Dispose() { }
        }

        private class UnitOfWorkWriteScope : UnitOfWorkScope, IUnitOfWorkWriteScope
        {
            public UnitOfWorkWriteScope(DataContext context)
                : base(context)
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            }

            async Task<int> IUnitOfWorkWriteScope.CompleteAsync() => await this.context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
