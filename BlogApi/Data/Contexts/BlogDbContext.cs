using BlogApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BlogApi.Data.Contexts
{
    public class BlogDbContext:DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }

        public override int SaveChanges()
        {
            HandleSoftDelete();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            HandleSoftDelete();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void HandleSoftDelete()
        {
            foreach (var entry in ChangeTracker.Entries<Post>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                }
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasQueryFilter(p=>!p.IsDeleted);
        }

    }
}
