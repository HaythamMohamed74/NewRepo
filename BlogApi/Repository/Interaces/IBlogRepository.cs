using BlogApi.Data.Entities;

namespace BlogApi.Repository.Interaces
{
    public interface IBlogRepository
    {
        public Task<IReadOnlyList<Post>>  GetAllPosts();
        public Task<Post>GetById(int id);
        public Task AddPost(Post post);
        //public Post UpdatePost(Post post);
        public Task<int> DeletePost(int id);


    }
}
