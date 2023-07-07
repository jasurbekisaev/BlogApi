using BlogApi.DtoModels.BlogDtoModels;
using BlogApi.Managers;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly BlogManager _manager;
    public BlogController(BlogManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBlogs()
    {
        var blogs = await _manager.GetBlogs();
        return Ok(blogs);
    }

    [HttpGet("GetBlogsByUserId/{UserId}")]
    public async Task<IActionResult> GetBlogsOfUser(Guid UserId)
    {
        var blogs = await _manager.GetBlogsByUserId(UserId);
        return Ok(blogs);
    }

    [HttpGet("GetBlogsByUsername/{Username}")]
    public async Task<IActionResult> GetAllBlogsByUsername(string Username)
    {
        var blogs = await _manager.GetBlogsByUsername(Username);
        return Ok(blogs);
    }

    [HttpGet("GetBlog/{blogId}")]
    public async Task<IActionResult> GetBlogById(Guid blogId)
    {
        var blog = await _manager.GetBlogByBlogId(blogId);
        return Ok(blog);
    }

    [HttpPost()]
    public async Task<IActionResult> CreateBlog([FromBody] CreateBlogDto model)
    {
        var blog = await _manager.CreateBlog(model);
        return Ok(blog);
    }

    [HttpDelete("{blogId}")]
    public async Task<IActionResult> DeleteBlog(Guid blogId)
    {
        return Ok(await _manager.DeleteBlog(blogId));
    }

    [HttpPut("{blogId}")]
    public async Task<IActionResult> UpdateBlog(Guid blogId, [FromBody] CreateBlogDto model)
    {
        return Ok(await _manager.UpdateBlog(blogId, model));
    }


}