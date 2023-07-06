using BlogApi.Entities;

namespace BlogApi.DtoModels.PostDtoModel;

public class CreatePostDto
{
    public required string PostTitle { get; set; }
    public required string Text { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public Guid BlogId { get; set; }
    public Blog Blog { get; set; }
}
