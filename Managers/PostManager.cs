using BlogApi.Context;
using BlogApi.DtoModels.PostDtoModel;
using BlogApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Managers;

public class PostManager
{
    private readonly BlogdbContext _context;

    public PostManager(BlogdbContext context)
    {
        _context = context;
    }

    public async Task<List<PostDto>> GetPosts()
    {
        var posts = await _context.Posts.Include(p => p.PostLikes).Include(p => p.SavedPosts).ToListAsync();

        return await ParseToList(posts);
    }

    public async Task<List<PostDto>> GetPostsOfBlog(Guid Id)
    {
        var posts = await _context.Posts.Where(p => p.BlogId == Id).Include(p => p.PostLikes).Include(p => p.SavedPosts).ToListAsync();

        if (posts == null)
        {
            throw new Exception("There are no posts of this blog");
        }
        return await ParseToList(posts);
    }

    public async Task<List<PostDto>> GetPostsOfUser(Guid UserId)
    {
        var user = await _context.Users.Where(u => u.UserId == UserId).FirstOrDefaultAsync();

        var blogs = user.UserBlogs;

        var posts = new List<Post>();

        foreach (var blog in blogs)
        {
            var BlogPosts = _context.Posts.Where(p => p.BlogId == blog.BlogId).ToList();
            if (BlogPosts == null)
            {
                continue;
            }
            foreach (var post in BlogPosts)
            {
                posts.Add(post);
            }
        }

        return await ParseToList(posts);
    }

    public async Task<PostDto> CreatePost(CreatePostDto model)
    {
        var post = new Post()
        {
            PostTitle = model.PostTitle,
            Text = model.Text,
            CreatedTime = model.CreatedTime,
            BlogId = model.BlogId
        };

        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return ParseToPost(post);
    }

    public async Task<PostDto> UpdatePost(Guid postId, CreatePostDto model)
    {
        var post = await _context.Posts.Where(u => u.PostId == postId).FirstOrDefaultAsync();
        if (post == null)
        {
            throw new Exception("There is no post");
        }

        post.PostTitle = model.PostTitle;
        post.Text = model.Text;
        await _context.SaveChangesAsync();
        return ParseToPost(post);
    }

    public async Task<string> DeletePost(Guid postId)
    {
        var post = await _context.Posts.Where(u => u.PostId == postId).FirstOrDefaultAsync();
        if (post == null)
        {
            throw new Exception("There is no post");
        }

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
        return "Deleted successfully";
    }

    private async Task<List<PostDto>> ParseToList(List<Post> posts)
    {
        var postModels = new List<PostDto>();
        foreach (var post in posts)
        {
            postModels.Add(ParseToPost(post));
        }

        return postModels;
    }

    private PostDto ParseToPost(Post post)
    {
        var postModel = new PostDto()
        {
            Text = post.Text,
            CreatedTime = post.CreatedTime,
            PostLikes = post.PostLikes,
            SavedPosts = post.SavedPosts,
            PostTitle = post.PostTitle,
        };
        return postModel;
    }
}
