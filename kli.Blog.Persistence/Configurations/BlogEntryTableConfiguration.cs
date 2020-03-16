using kli.Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace kli.Blog.Persistence.Configurations
{
    public class BlogEntryTableConfiguration : IEntityTypeConfiguration<BlogEntry>
    {
        public void Configure(EntityTypeBuilder<BlogEntry> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Header).IsRequired();
            builder.Property(e => e.Intro).IsRequired();
            builder.Property(e => e.Content).IsRequired();
            builder.Property(e => e.Published).IsRequired();
            builder.Property(e => e.IsPublished).IsRequired().HasDefaultValue(false);
        }
    }
}
