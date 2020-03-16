using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kli.Blog.Core.Contracts.Data
{
    public interface IUnitOfWork
    {
        IUnitOfWorkScope Begin();
        IUnitOfWorkWriteScope BeginWrite();
    }

    public interface IUnitOfWorkScope : IDisposable
    {
        IRepository<T> SetOf<T>() where T : class;
    }

    public interface IUnitOfWorkWriteScope : IUnitOfWorkScope
    {
        Task<int> CompleteAsync();
    }
}
