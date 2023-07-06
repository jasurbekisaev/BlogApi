namespace BlogApi.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public required string Text { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public Guid PostId { get; set; }
    public virtual Post Post { get; set; }
}
