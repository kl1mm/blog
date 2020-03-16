using System.Threading.Tasks;
using kli.Blog.Core.Contracts.Data;
using kli.Blog.Persistence;
using Microsoft.EntityFrameworkCore;

namespace kli.Blog.Core
{
   internal class DataStoreInitializer : IDataStoreInitializer
    {
        private readonly DataContext dataContext;

        public DataStoreInitializer(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        async Task IDataStoreInitializer.MigrateAsync()
        {
            await this.dataContext.Database.MigrateAsync().ConfigureAwait(false);
            // Seeding
        }
    }
}
