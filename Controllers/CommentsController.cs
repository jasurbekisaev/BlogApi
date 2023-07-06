using AutoMapper;
using BlogApi.DtoModels.CommentDtoModel;
using BlogApi.Managers;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CommentManager _manager;

        public CommentsController(CommentManager manager, IMapper mapper)
        {
            _mapper = mapper;
            _manager = manager;
        }

        [HttpGet("PostId")]
        public async Task<IActionResult> GetCommentsByPostId(Guid PostId)
        {
            var comments = _mapper.Map<List<CommentDto>>(_manager.GetCommentsByBlogId(PostId));

            return Ok(comments);
        }
    }
}
