using BlogApi.Entities;

namespace BlogApi.DtoModels.UserDtoModel;

public class UserDto
{
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public string UserName { get; set; }

    public UserDto(User user)
    {
        UserId = user.UserId;
        Name = user.Name;
        UserName = user.UserName;
    }
}
