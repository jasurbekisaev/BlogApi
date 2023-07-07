using AutoMapper;
using BlogApi.DtoModels.CommentDtoModel;
using BlogApi.Entities;
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

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto model)
        {
            if (model == null)
            {
                return BadRequest(ModelState);
            }

            var commentMap = _mapper.Map<Comment>(model);
            var comment = await _manager.CreateComment(commentMap);

            return Ok(comment);
        }
    }
}
