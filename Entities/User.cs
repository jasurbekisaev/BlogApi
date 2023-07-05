namespace BlogApi.Entities;

public class User
{
    public Guid UserId { get; set; }
    public required string Name { get; set; }
    public string? Surname { get; set; }
    public required string UserName { get; set; }
    public string PasswordHash { get; set; }

    public List<Blog> UserBlogs { get; set; }
    public List<SavedPosts> SavedPosts { get; set; }

}
