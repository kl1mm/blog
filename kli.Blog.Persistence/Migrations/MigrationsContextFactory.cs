using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace kli.Blog.Persistence.Migrations
{
    internal class MigrationsContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        DataContext IDesignTimeDbContextFactory<DataContext>.CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var dbContextBuilder = new DbContextOptionsBuilder<DataContext>();
            dbContextBuilder.UseSqlite(configuration.GetConnectionString("default"),
                options =>
                {
                    options.MigrationsAssembly(typeof(DataContext).Assembly.FullName);
                });

            return new DataContext(dbContextBuilder.Options);
        }
    }
}
