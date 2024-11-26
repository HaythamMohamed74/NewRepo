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

        public BlogRepository( BlogDbContext blogDbContext) {
            _blogDbContext = blogDbContext;
        }



        public async Task<IReadOnlyList<Post>> GetAllPosts()
        {

            var posts = await _blogDbContext.Set<Post>().AsNoTracking().ToListAsync();
            if (posts==null)
            {
                throw new Exception("No Posts Found ");
            }
            return posts;


        }
        public async Task AddPost(Post post)
        {
            try {

                _blogDbContext.Set<Post>().Add(post);
                await _blogDbContext.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception("An Error occured when add new post Post ");
            }
            
       
        }

     
     

        public async Task<Post> GetById(int id)
        {
            var post=_blogDbContext.Set<Post>().FindAsync(id);
            if (post == null)
            {
                throw new KeyNotFoundException($"No Todo item with Id {id} found.");
            }
            return await post;

        }

        public async Task DeletePost(int id)
        {
            var post = await _blogDbContext.Set<Post>().FindAsync(id);
            if (post == null)
            {
                throw new Exception($"Post with Id ={id} not Found ");
            }
            _blogDbContext.Set<Post>().Remove(post);
            await _blogDbContext.SaveChangesAsync();
            

        }

        public  async Task<Post> UpdatePost(Post  post)  // update by id
        {
            //get post
            //check if post not null
            //update it
            var postT= await _blogDbContext.Set<Post>().FindAsync(post.Id);
            if (post is not null)
            {
                post.AuthorName = postT.AuthorName;
                post.Content = postT.Content;
                post.Title = postT.Title;
                return   post;

            }
            return  null;
        }
    }
}
