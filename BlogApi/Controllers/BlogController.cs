using BlogApi.Data.Entities;
using BlogApi.Repository.Interaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpGet]
        public async Task<ActionResult>GetPosts() 
        { 
           return Ok(await _blogRepository.GetAllPosts());
        
        }

        [HttpGet]
        public async Task<ActionResult> GetPostById(int id)
        {
           return Ok(await _blogRepository.GetById(id));

        }
        [HttpDelete("id")]
        public  async Task<ActionResult> DeletePost(int id)
        {
           return Ok(await _blogRepository.DeletePost(id));

        }
        [HttpPost]
        public async Task AddPost(Post post)
        {
             await _blogRepository.AddPost(post);

        }





    }
}
