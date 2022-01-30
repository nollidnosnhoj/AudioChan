﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Audiochan.Core.Commons.CQRS;
using Audiochan.Core.Commons.Dtos.Wrappers;
using Audiochan.Core.Commons.Extensions;

using Audiochan.Core.Features.Audios.Models;
using Audiochan.Core.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Audiochan.Core.Features.Users.Queries.GetUserAudios
{
    public record GetUsersAudioQuery(string Username) : IQueryRequest, IRequest<OffsetPagedListDto<AudioDto>>
    {
        public int Offset { get; init; } = 1;
        public int Size { get; init; } = 30;
    }

    public class GetUsersAudioQueryHandler : IRequestHandler<GetUsersAudioQuery, OffsetPagedListDto<AudioDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUsersAudioQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OffsetPagedListDto<AudioDto>> Handle(GetUsersAudioQuery request,
            CancellationToken cancellationToken)
        {
            var list = await _dbContext.Users
                .Where(u => u.UserName == request.Username)
                .SelectMany(u => u.Audios)
                .OrderByDescending(a => a.Id)
                .ProjectTo<AudioDto>(_mapper.ConfigurationProvider)
                .OffsetPaginateAsync(request.Offset, request.Size, cancellationToken);
            return new OffsetPagedListDto<AudioDto>(list, request.Offset, request.Size);
        }
    }
}