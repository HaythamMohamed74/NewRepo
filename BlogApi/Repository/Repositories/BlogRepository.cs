using BlogApi.Data.Contexts;
using BlogApi.Data.Entities;
using BlogApi.Repository.Interaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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
            return await _blogDbContext.Set<Post>().AsNoTracking().ToListAsync();
            

        }
        public async Task AddPost(Post post)
        {
           
           await _blogDbContext.Set<Post>().AddAsync(post);
            await _blogDbContext.SaveChangesAsync();
        }

     
     

        public async Task<Post> GetById(int id)
        {
           return await _blogDbContext.Set<Post>().FirstOrDefaultAsync(p=>p.Id==id);

        }

        public async Task<int> DeletePost(int id)
        {
            var entity = _blogDbContext.Set<Post>().Find(id);
            _blogDbContext.Set<Post>().Remove(entity);
            //return _blogDbContext.SaveChangesAsync();
            //await  _blogDbContext.Set<Post>().Where(p => p.Id == 1).ExecuteDeleteAsync();
            await _blogDbContext.SaveChangesAsync();
            return id;

        }

        //public  Post UpdatePost(Post post)
        //{
        //    return  _blogDbContext.Set<Post>()(post);
        //}
    }
}
