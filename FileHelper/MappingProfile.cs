using AutoMapper;
using BlogApi.DtoModels.CommentDtoModel;
using BlogApi.DtoModels.PostDtoModel;
using BlogApi.DtoModels.SavedPostDtoModel;
using BlogApi.Entities;

namespace BlogApi.FileHelper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentDto>();
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<Comment, CreateCommentDto>();
            CreateMap<CommentDto, Comment>();
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();
            CreateMap<Post, SavedPosts>();
            CreateMap<SavedPosts, SavedPostDto>();
        }
    }
}
