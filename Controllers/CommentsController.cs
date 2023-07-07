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

        [HttpGet("CommentsofPost/{PostId}")]
        public async Task<IActionResult> GetCommentsByPostId(Guid PostId)
        {
            var comments = await _manager.GetCommentsByPostId(PostId);

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

        [HttpGet("CommentsOfUser/{UserId}")]
        public async Task<IActionResult> GetCommentsByUserId(Guid Id)
        {
            var comments = await _manager.GetCommentsByUserId(Id);
            if (comments == null)
            {
                return BadRequest(null);
            }
            return Ok(comments);
        }

        [HttpGet("Comment/{id}")]
        public async Task<IActionResult> GetCommentById(Guid id)
        {
            var comment = await _manager.GetCommentById(id);
            return Ok(comment);
        }
        [HttpPut]
        public async Task<string> UpdateComment(Guid Id, string text)
        {
            var newcomment = await _manager.UpdateComment(Id, text);
            return newcomment.Text;
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteComment(Guid Id)
        {
            var comment = await _manager.GetCommentById(Id);
            if (comment == null)
            {
                return NotFound();
            }
            await _manager.DeleteComment(Id);
            return Ok();
        }

    }
}
