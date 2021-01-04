﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Audiochan.Core.Common.Enums;
using Audiochan.Core.Common.Extensions;
using Audiochan.Core.Common.Mappings;
using Audiochan.Core.Common.Models;
using Audiochan.Core.Entities;
using Audiochan.Core.Features.Followers.Models;
using Audiochan.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Audiochan.Core.Features.Followers
{
    public class FollowerService : IFollowerService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IAudiochanContext _dbContext;

        public FollowerService(ICurrentUserService currentUserService, IAudiochanContext dbContext)
        {
            _currentUserService = currentUserService;
            _dbContext = dbContext;
        }

        public async Task<List<FollowUserViewModel>> GetUsersFollowers(string username, 
            PaginationQuery paginationQuery, CancellationToken cancellationToken = default)
        {
            var currentUserId = _currentUserService.GetUserId();

            return await _dbContext.FollowedUsers
                .AsNoTracking()
                .Include(u => u.Target)
                .Include(u => u.Observer)
                .ThenInclude(u => u.Followers)
                .Where(u => u.Target.UserName == username)
                .Select(MapProjections.FollowUser(currentUserId))
                .Paginate(paginationQuery, cancellationToken);
        }

        public async Task<List<FollowUserViewModel>> GetUsersFollowings(string username, 
            PaginationQuery paginationQuery, CancellationToken cancellationToken = default)
        {
            var currentUserId = _currentUserService.GetUserId();

            return await _dbContext.FollowedUsers
                .AsNoTracking()
                .Include(u => u.Observer)
                .Include(u => u.Target)
                .ThenInclude(u => u.Followers)
                .Where(u => u.Observer.UserName == username)
                .Select(MapProjections.FollowUser(currentUserId))
                .Paginate(paginationQuery, cancellationToken);
        }

        public async Task<bool> CheckFollowing(long userId, string username, 
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.FollowedUsers
                .AsNoTracking()
                .Include(u => u.Target)
                .AnyAsync(u => u.ObserverId == userId 
                               && u.Target.UserName == username.ToLower(), cancellationToken);
        }

        public async Task<IResult<FollowUserViewModel>> Follow(string username, CancellationToken cancellationToken = default)
        {
            var target = await _dbContext.Users.AsNoTracking()
                .SingleOrDefaultAsync(u => u.UserName == username.ToLower(), cancellationToken);

            if (target == null)
                return Result<FollowUserViewModel>.Fail(ResultErrorCode.NotFound);

            var currentUserId = await _dbContext.Users
                .AsNoTracking()
                .Select(u => u.Id)
                .SingleOrDefaultAsync(id => id == _currentUserService.GetUserId(), cancellationToken);

            var followedUser = await _dbContext.FollowedUsers
                .SingleOrDefaultAsync(u => u.TargetId == target.Id 
                                           && u.ObserverId == currentUserId, cancellationToken);

            if (followedUser == null)
            {
                followedUser = new FollowedUser
                {
                    TargetId = target.Id,
                    ObserverId = currentUserId,
                };

                await _dbContext.FollowedUsers.AddAsync(followedUser, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Result<FollowUserViewModel>.Success(FollowUserViewModel.From(followedUser, currentUserId));
        }

        public async Task<IResult> Unfollow(string username, CancellationToken cancellationToken = default)
        {
            var target = await _dbContext.Users.AsNoTracking()
                .SingleOrDefaultAsync(u => u.UserName == username.ToLower(), cancellationToken);

            if (target == null)
                return Result.Fail(ResultErrorCode.NotFound);

            var currentUserId = await _dbContext.Users
                .AsNoTracking()
                .Select(u => u.Id)
                .SingleOrDefaultAsync(id => id == _currentUserService.GetUserId(), cancellationToken);

            var followedUser = await _dbContext.FollowedUsers
                .SingleOrDefaultAsync(u => u.TargetId == target.Id 
                                           && u.ObserverId == currentUserId, cancellationToken);

            if (followedUser != null)
            {
                _dbContext.FollowedUsers.Remove(followedUser);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Result.Success();
        }
    }
}