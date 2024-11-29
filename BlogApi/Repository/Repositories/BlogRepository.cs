using BlogApi.Data.Contexts;
using BlogApi.Data.Entities;
using BlogApi.Repository.Interaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace BlogApi.Repository.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDbContext _blogDbContext;
        private readonly ILogger<BlogRepository> _logger;

        public BlogRepository(BlogDbContext blogDbContext, ILogger<BlogRepository> logger)
        {
            _blogDbContext = blogDbContext;
            _logger = logger;
        }



        public async Task<IReadOnlyList<Post>> GetAllPosts()
        {

            _logger.LogInformation(" All Posts are Get Succsefully");
            return await _blogDbContext.Set<Post>().AsNoTracking().ToListAsync();


        }
        public async Task AddPost(Post post)
        {
            if (post == null)
            {
                _logger.LogWarning("Attempted to add a null post.");
                throw new ArgumentNullException(nameof(post));
            }

            await _blogDbContext.Set<Post>().AddAsync(post);
            await _blogDbContext.SaveChangesAsync();
            _logger.LogInformation("Post are created Succesfully with {id}  ", post.Id);


        }




        public async Task<Post?> GetById(int id)
        {
            _logger.LogInformation("Fetching post with ID {Id}.", id);
            var post = await _blogDbContext.Set<Post>().FindAsync(id);
            if (post == null)
            {
                _logger.LogWarning("No post item with #{id} found", id);
                //throw new KeyNotFoundException($"No [post] item with Id {id} found.");
            }
            _logger.LogInformation("post with {id}  are Returned Succsefullty ", id);
            return post;

        }

        public async Task DeletePost(int id)
        {
            var post = await GetById(id);
            if (post == null)
            {
                _logger.LogError("Post with {id} is not Found", id);
            }
            _blogDbContext.Set<Post>().Remove(post);
            await _blogDbContext.SaveChangesAsync();
            _logger.LogInformation("Successfully deleted post with ID {Id}.", id);


        }

        public async Task<bool> UpdatePost(Post post)  // update by id
        {
            if (post == null)
            {
                _logger.LogWarning("Attempted to update a null post");
            }
            _blogDbContext.Set<Post>().Update(post);
            await _blogDbContext.SaveChangesAsync();
            _logger.LogInformation($"Updated post: Succfully {post}");
            return true;


        }

        //public async Task UpdatePost(Post post)
        //{
        //    if (post == null) throw new ArgumentNullException(nameof(post));

        //    var existingPost = await GetById(post.Id);
        //    if (existingPost == null)
        //        throw new KeyNotFoundException($"Post with Id {post.Id} not found.");

        //    _blogDbContext.Entry(existingPost).CurrentValues.SetValues(post);
        //    await _blogDbContext.SaveChangesAsync();
        //}
    }


  

   



}
