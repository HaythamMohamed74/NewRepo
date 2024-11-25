using BlogApi.Data.Contexts;
using BlogApi.Data.Entities;
using Microsoft.IdentityModel.Tokens;

namespace BlogApi.Helper
{
    public static class SeedingData
    {
        public static void SeedData(BlogDbContext blogDbContext )
        {
            List<Post> posts = new List<Post>()
            {
                new Post{  AuthorName ="Haytham", Content ="just seeding data for Haytham Author", Title ="HardWork",IsDeleted=false},
                new Post { AuthorName ="Ahmed", Content ="just seeding data for Haytham Author", Title ="Make all Effort",IsDeleted=false},

            };

            if (blogDbContext.Posts is not null && !blogDbContext.Posts.Any())
            {
               
                    blogDbContext.Set<Post>().AddRange(posts);
                   blogDbContext.SaveChanges();

            }
          



            //if (blogDbContext.Set<Post>().IsNullOrEmpty()) { 
            //    blogDbContext.Set<Post>().AddRange( posts );
            //    blogDbContext.SaveChanges();

            //}

        }
    }
}
