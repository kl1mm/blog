using System.Threading.Tasks;

namespace kli.Blog.Core.Contracts.Data
{
    public interface IDataStoreInitializer
    {
        Task MigrateAsync();
    }
}
