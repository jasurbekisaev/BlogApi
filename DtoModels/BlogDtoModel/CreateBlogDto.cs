namespace BlogApi.DtoModels.BlogDtoModels;

public class CreateBlogDto
{
    public required string Title { get; set; }
    public required string Tag { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.Now;

}
