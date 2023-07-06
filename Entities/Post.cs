namespace BlogApi.Entities;

public class Post
{
    public Guid PostId { get; set; }
    public required string PostTitle { get; set; }
    public required string Text { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public Guid BlogId { get; set; }
    public virtual Blog Blog { get; set; }
    public virtual List<Like>? PostLikes { get; set; }
    public virtual List<SavedPosts>? SavedPosts { get; set; }
    public virtual List<Comment>? PostComments { get; set; }

}
