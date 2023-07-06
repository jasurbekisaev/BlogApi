namespace BlogApi.DtoModels.CommentDtoModel
{
    public class CreateCommentDto
    {
        public required string Text { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
    }
}
