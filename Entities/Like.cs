﻿namespace BlogApi.Entities;

public class Like
{
    public Guid LikeId { get; set; }
    public Guid PostId { get; set; }
    public virtual Post Post { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}
