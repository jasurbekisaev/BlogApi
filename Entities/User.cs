namespace BlogApi.Entities;

public class User
{
    public Guid UserId { get; set; }
    public required string Name { get; set; }
    public string? Surname { get; set; }
    public required string UserName { get; set; }
    public string PasswordHash { get; set; }
    public virtual List<Blog> UserBlogs { get; set; }

}
