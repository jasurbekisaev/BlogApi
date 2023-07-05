using BlogApi.Context;
using BlogApi.DtoModels.BlogDtoModels;
using BlogApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Managers;

public class BlogManager
{
    private readonly BlogdbContext _context;

    public BlogManager(BlogdbContext context)
    {
        _context = context;
    }

    public async Task<List<BlogDto>> GetBlogsByUserId()
    {
        var blogs = await _context.Blogs.Include(b => b.BlogPosts).ToListAsync();

        return ParseList(blogs);
    }

    public async Task<BlogDto> GetBlogByBlogId(Guid Id)
    {
        var blog = await _context.Blogs.Where(b => b.BlogId == Id).FirstOrDefaultAsync();
        if (blog == null)
        {
            return null;
        }
        return ParseToBlogDto(blog);
    }

    public async Task<List<BlogDto>> GetBlogsByUsername(string Username)
    {
        var user = await _context.Users.Where(u => u.UserName == Username).FirstOrDefaultAsync();
        if (user == null)
        {
            throw new Exception("There is user with this username");
        }
        var blogs = user.UserBlogs;

        if (blogs == null)
        {
            throw new Exception("There are no blogs of this user");
        }
        return ParseList(blogs);
    }


    public async Task<List<BlogDto>> GetBlogsByUserId(Guid Id)
    {
        var blogs = await _context.Blogs.Where(b => b.UserId == Id).Include(b => b.BlogPosts).ToListAsync();
        return ParseList(blogs);
    }

    public async Task<BlogDto> CreateBlog(CreateBlogDto model)
    {
        var blog = new Blog()
        {
            Title = model.Title,
            Tag = model.Tag,
            CreatedTime = model.CreatedTime,
            UserId = model.UserId,
        };
        await _context.Blogs.AddAsync(blog);
        await _context.SaveChangesAsync();
        return ParseToBlogDto(blog)!;
    }

    public async Task<BlogDto> UpdateBlog(Guid Id, CreateBlogDto blogDto)
    {
        var blog = await _context.Blogs.Where(b => b.BlogId == Id).FirstOrDefaultAsync();
        if (blog == null)
        {
            throw new Exception("There is no Blog");
        }

        blog.Title = blogDto.Title;
        blog.Tag = blogDto.Tag;
        await _context.SaveChangesAsync();
        return ParseToBlogDto(blog)!;
    }

    public async Task<string> DeleteBlog(Guid Id)
    {
        var blog = await _context.Blogs.Where(b => b.BlogId == Id).FirstOrDefaultAsync();
        if (blog == null)
        {
            return "There is no Blog";
        }

        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();
        return "deleted successfully";
    }
    private BlogDto? ParseToBlogDto(Blog blog)
    {
        var blogModel = new BlogDto()
        {
            BlogId = blog.BlogId,
            Tag = blog.Tag,
            Title = blog.Title,
            UserId = blog.UserId,
            CreatedTime = blog.CreatedTime,
            BlogPosts = blog.BlogPosts,
        };
        return blogModel;
    }
    private List<BlogDto> ParseList(List<Blog> blogs)
    {
        var blogModels = new List<BlogDto>();
        foreach (var blog in blogs)
        {
            blogModels.Add(ParseToBlogDto(blog));
        }
        return blogModels;
    }
}
