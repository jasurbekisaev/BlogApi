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

    public async Task<List<CommentDto>> GetCommentsByUserId(Guid UserId)
    {
        var user = await _context.Users.Where(u => u.UserId == UserId).FirstOrDefaultAsync();
        if (user == null)
        {
            throw new Exception("There is no user with this Id");
        }

        var comments = await _context.Comments.Where(p => p.UserId == UserId).ToListAsync();
        if (comments == null)
        {
            throw new Exception("there are no comments of this user");
        }
        var commentDtos = _mapper.Map<List<CommentDto>>(comments);
        return commentDtos;
    }

    public async Task<CommentDto> GetCommentById(Guid Id)
    {
        var comment = await _context.Comments.Where(c => c.Id == Id).FirstOrDefaultAsync();
        if (comment == null)
        {
            throw new Exception(" there is no comment");
        }

        var commentDto = _mapper.Map<CommentDto>(comment);
        return commentDto;
    }
    public async Task<CommentDto> UpdateComment(Guid CommentId, string text)
    {
        var comment = await _context.Comments.Where(c => c.Id == CommentId).FirstOrDefaultAsync();
        comment.Text = text;
        var commentDto = _mapper.Map<CommentDto>(comment);

        await _context.SaveChangesAsync();
        return commentDto;
    }

    public async Task<bool> DeleteComment(Guid Id)
    {
        var comment = await _context.Comments.Where(c => c.Id == Id).FirstOrDefaultAsync();
        if (comment == null)
        {
            throw new Exception("there is no comment with this Id");
        }
        _context.Remove(comment);
        await _context.SaveChangesAsync();
        return await Save();
    }

    private async Task<bool> Save()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0 ? true : false;
    }


}
