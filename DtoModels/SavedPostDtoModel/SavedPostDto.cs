namespace BlogApi.DtoModels.SavedPostDtoModel;

public class SavedPostDto
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
}
