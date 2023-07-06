using BlogApi.Entities;

namespace BlogApi.DtoModels.LikeDtoModel;

public class LikeDto
{
    public Guid LikeId { get; set; }
    public Guid PostId { get; set; }
    public Post Post { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}
