using AutoMapper;
using BlogApi.Context;
using BlogApi.DtoModels.CommentDtoModel;
using BlogApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Managers;

public class CommentManager
{
    private readonly BlogdbContext _context;
    private readonly IMapper _mapper;

    public CommentManager(BlogdbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<CommentDto>> GetCommentsByPostId(Guid id)
    {
        var post = await _context.Posts.Where(p => p.PostId == id).FirstOrDefaultAsync();
        if (post == null)
        {
            throw new Exception("there is no post");
        }
        var comments = post.PostComments;
        var commentDtos = _mapper.Map<List<CommentDto>>(comments);
        return commentDtos;
    }
    public async Task<Comment> CreateComment(Comment model)
    {
        var post = await _context.Posts.Where(u => u.PostId == model.PostId).FirstOrDefaultAsync();
        if (post == null)
        {
            throw new Exception("There is no post with this Id");
        }

        _context.Comments.Add(model);
        await _context.SaveChangesAsync();
        return model;
    }


}
