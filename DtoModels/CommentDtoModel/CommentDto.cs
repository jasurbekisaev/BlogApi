namespace BlogApi.DtoModels.CommentDtoModel;

public class CommentDto
{
    public Guid Id { get; set; }
    public required string Text { get; set; }
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
}
