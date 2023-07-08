using BlogApi.Managers;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedPostsController : ControllerBase
    {
        private readonly UserManager _userManager;
        private readonly SavedPostManager _savedPostManager;

        public SavedPostsController(UserManager userManager, SavedPostManager savedPostManager)
        {
            _userManager = userManager;
            _savedPostManager = savedPostManager;
        }


        [HttpPost]
        public async Task<IActionResult> SavePost(Guid PostId, Guid UserId)
        {
            var savedPostDto = await _savedPostManager.SavePosts(PostId, UserId);
            if (savedPostDto != null)
            {
                return Ok(savedPostDto);
            }
            else
            {
                return BadRequest("Failed to save the post.");
            }
        }

        [HttpGet("UserId")]
        public async Task<IActionResult> GetSavedPostsByUserId(Guid UserId)
        {
            return Ok(await _savedPostManager.GetSavedPostsByUserId(UserId));
        }

    }
}
