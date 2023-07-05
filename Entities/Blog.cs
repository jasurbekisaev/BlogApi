namespace BlogApi.Entities;

public class Blog
{
    public Guid BlogId { get; set; }
    public required string Title { get; set; }
    public required string Tag { get; set; }
    public DateTime CreatedTime { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public virtual List<Post>? BlogPosts { get; set; }

}
