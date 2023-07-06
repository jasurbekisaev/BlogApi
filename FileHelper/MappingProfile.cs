﻿using AutoMapper;
using BlogApi.DtoModels.CommentDtoModel;
using BlogApi.Entities;

namespace BlogApi.FileHelper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentDto>();
        }
    }
}