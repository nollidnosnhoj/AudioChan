﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Audiochan.Core.Common.Extensions;
using Audiochan.Core.Common.Extensions.MappingExtensions;
using Audiochan.Core.Common.Interfaces;
using Audiochan.Core.Common.Models.Interfaces;
using Audiochan.Core.Common.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Audiochan.Core.Features.Followers.GetFollowers
{
    public record GetUserFollowersRequest : IHasPage, IRequest<PagedList<FollowerViewModel>>
    {
        public string Username { get; init; }
        public int Page { get; init; }
        public int Size { get; init; }
    }
    
    public class GetUserFollowersRequestHandler : IRequestHandler<GetUserFollowersRequest, PagedList<FollowerViewModel>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetUserFollowersRequestHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedList<FollowerViewModel>> Handle(GetUserFollowersRequest request,
            CancellationToken cancellationToken)
        {
            return await _dbContext.FollowedUsers
                .AsNoTracking()
                .Include(u => u.Target)
                .Include(u => u.Observer)
                .Where(u => u.Target.UserName == request.Username.Trim().ToLower())
                .ProjectToFollower()
                .PaginateAsync(request, cancellationToken);
        }
    }
}