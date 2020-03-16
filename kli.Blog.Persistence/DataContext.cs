using Microsoft.EntityFrameworkCore;

namespace kli.Blog.Persistence
{
    internal class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
