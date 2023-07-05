using BlogApi.Entities;

namespace BlogApi.DtoModels.PostDtoModel;

public class PostDto
{
    public Guid PostId { get; set; }
    public required string PostTitle { get; set; }
    public required string Text { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.Now;
    public Guid BlogId { get; set; }
    public virtual Blog Blog { get; set; }
    public virtual List<Like>? PostLikes { get; set; }
    public virtual List<SavedPosts>? SavedPosts { get; set; }

}
