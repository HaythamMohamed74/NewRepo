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

        

    }
}
