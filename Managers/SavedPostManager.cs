using AutoMapper;
using BlogApi.Context;
using BlogApi.DtoModels.SavedPostDtoModel;
using BlogApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Managers;

public class SavedPostManager
{
    private readonly BlogdbContext _context;
    private readonly IMapper _mapper;
    public SavedPostManager(BlogdbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<SavedPostDto> SavePosts(Guid postId, Guid userId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == postId);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

        if (post == null || user == null)
        {
            return null;
        }
        var savedPost = await _context.SavedPosts
            .FirstOrDefaultAsync(s => s.PostId == postId && s.UserId == userId);

        if (savedPost == null)
        {
            savedPost = new SavedPosts()
            {
                PostId = postId,
                UserId = userId,
                Post = post,
                User = user,
            };

            _context.SavedPosts.Add(savedPost);
            await _context.SaveChangesAsync();

            var savedPostDto = _mapper.Map<SavedPostDto>(savedPost);
            return savedPostDto;
        }
        else
        {
            _context.SavedPosts.Remove(savedPost);
            await _context.SaveChangesAsync();
            return null;
        }
    }


    public async Task<List<SavedPosts>> GetSavedPostsByUserId(Guid UserId)
    {
        var savedPosts = await _context.SavedPosts.Where(s => s.UserId == UserId).ToListAsync();
        return savedPosts;
    }
}

