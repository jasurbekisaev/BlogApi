using BlogApi.Entities;

namespace BlogApi.DtoModels.SavedPostDtoModel;

public class SavedPostDto
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public virtual Post Post { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}
