using AutoMapper;
using BlogApi.Context;
using BlogApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Managers;

public class CommentManager
{
    private readonly BlogdbContext _context;
    private readonly IMapper _mapper;

    public CommentManager(BlogdbContext context)
    {
        _context = context;
    }

    public async Task<List<Comment>> GetCommentsByBlogId(Guid id)
    {
        var comments = await _context.Comments.Where(c => c.PostId == id).ToListAsync();

        return comments;
    }

}
