using Azure.Core;
using BlogApi.Data.Entities;
using BlogApi.Repository.Interaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

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
        public async Task<IActionResult> GetPosts() 
        {
           
            try
            {
                var posts = await _blogRepository.GetAllPosts();
                if (posts is null || !posts.Any())
                {
                     return Ok("No Posts Found ");
                }
                return Ok(await _blogRepository.GetAllPosts());
            }
            catch (Exception ex) {
                return StatusCode(500, new { message = "An error occurred while retrieving all posts", error = ex.Message });
            }


        }

        [HttpGet]
        public async Task<IActionResult> GetPostById(int id)
        {
            try
            {
                var post = await _blogRepository.GetById(id);
                if (post == null)
                {
                    return NotFound(new { message = $"No post item with Id {id} found." });
                }
                return Ok(new { message = $"Successfully retrieved post item with Id {id}.", data = post });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while retrieving the Todo item with Id {id}.", error = ex.Message });
            }
           
        


          
            
            
           

        }
        [HttpDelete("id")]
        public  async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                await _blogRepository.DeletePost(id);
                return Ok(new { message = $"Todo  with id {id} successfully deleted" });
            }
            catch (Exception ex) { 
            
            return StatusCode(500, new { error = ex.Message });
            }
         

        }
        [HttpPost]
        public async Task<IActionResult> AddPost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {

                await _blogRepository.AddPost(post);
                return Ok(new { message = "Blog post successfully created" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the  crating Todo Item", error = ex.Message });

            }





        }
           [HttpPut]
        public async Task Update(Post post)
        {
          
                Ok(await _blogRepository.UpdatePost(post));
            


        }






    }
}
