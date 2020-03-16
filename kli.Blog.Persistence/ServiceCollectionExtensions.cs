using System;
using kli.Blog.Core;
using kli.Blog.Core.Contracts.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace kli.Blog.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services
                .AddDbContextPool<DataContext>((sp, builder) =>
            {
                var connectionString = configuration.GetConnectionString("default");
                builder.UseSqlite(connectionString, options => options.MigrationsAssembly(typeof(DataContext).Assembly.FullName));
                builder.UseLoggerFactory(sp.GetService<ILoggerFactory>());
                builder.EnableSensitiveDataLogging();
            })
            .AddTransient<IDataStoreInitializer, DataStoreInitializer>()
            .AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
