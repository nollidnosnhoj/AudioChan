﻿using System.Linq;
using Audiochan.Core.Dtos;
using Audiochan.Core.Extensions;
using Audiochan.Core.Helpers;
using Audiochan.Domain.Entities;
using AutoMapper;

namespace Audiochan.API.Features.Audios.Mappings;

public class AudioDtoMapping : Profile
{
    public AudioDtoMapping()
    {
        long? userId = null;
        this.CreateStrictMap<Audio, AudioDto>()
            .ForMember(dest => dest.Description, c =>
                c.NullSubstitute(""))
            .ForMember(dest => dest.Slug, c =>
                c.MapFrom(src => HashIdHelper.EncodeLong(src.Id)))
            .ForMember(dest => dest.Src, c =>
                c.MapFrom(src => src.File))
            .ForMember(dest => dest.IsFavorited, c =>
                c.MapFrom(src => userId > 0 ? src.FavoriteAudios.Any(fa => fa.UserId == userId) : (bool?)null));
    }
}