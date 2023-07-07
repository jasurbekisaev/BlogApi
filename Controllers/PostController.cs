using BlogApi.DtoModels.PostDtoModel;
using BlogApi.Managers;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostManager _manager;

        public PostController(PostManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _manager.GetPosts();
            return Ok(posts);
        }

        [HttpGet("GetPostsOfBlog/{BlogId}")]
        public async Task<IActionResult> GetPostsOfBlog(Guid BlogId)
        {
            var posts = await _manager.GetPostsOfBlog(BlogId);

            return Ok(posts);
        }

        [HttpGet("GetPostsOfUser/{UserId}")]
        public async Task<IActionResult> GetPostsOfUser(Guid UserId)
        {
            var posts = await _manager.GetPostsOfUser(UserId);
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDto model)
        {
            var post = await _manager.CreatePost(model);
            return Ok(post);
        }

        [HttpPut("PostId")]
        public async Task<IActionResult> UpdatePost(Guid PostId, [FromBody] CreatePostDto model)
        {
            var post = await _manager.UpdatePost(PostId, model);
            return Ok(post);
        }

        [HttpDelete("PostId")]
        public async Task<IActionResult> DeletePost(Guid PostId)
        {
            return Ok(await _manager.DeletePost(PostId));
        }

    }
}
