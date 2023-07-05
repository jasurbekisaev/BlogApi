using BlogApi.Entities;

namespace BlogApi.DtoModels.BlogDtoModels;

public class BlogDto
{
    public Guid BlogId { get; set; }
    public required string Title { get; set; }
    public required string Tag { get; set; }
    public DateTime CreatedTime { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public List<Post>? BlogPosts { get; set; }

}
