﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Audiochan.Core.Entities;
using Audiochan.Core.Enums;
using Audiochan.Core.Interfaces;
using Audiochan.Core.Models.Interfaces;
using Audiochan.Core.Models.Responses;
using Audiochan.Core.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Audiochan.Features.Auth.Refresh
{
    public class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, IResult<AuthResultViewModel>>
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenProvider _tokenProvider;
        private readonly IDateTimeProvider _dateTimeProvider;

        public RefreshTokenRequestHandler(UserManager<User> userManager, ITokenProvider tokenProvider,
            IDateTimeProvider dateTimeProvider)
        {
            _userManager = userManager;
            _tokenProvider = tokenProvider;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<IResult<AuthResultViewModel>> Handle(RefreshTokenRequest request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
                return Result<AuthResultViewModel>
                    .Fail(ResultError.BadRequest, "Refresh token was not defined.");

            var user = await _userManager.Users
                .Include(u => u.RefreshTokens)
                .SingleOrDefaultAsync(u => u.RefreshTokens
                    .Any(t => t.Token == request.RefreshToken && t.UserId == u.Id), cancellationToken);

            if (user == null)
                return Result<AuthResultViewModel>.Fail(ResultError.BadRequest,
                    "Refresh token does not belong to a user.");

            var existingRefreshToken = user.RefreshTokens
                .Single(r => r.Token == request.RefreshToken);
            
            if (!await _tokenProvider.ValidateRefreshToken(existingRefreshToken.Token))
                return Result<AuthResultViewModel>.Fail(ResultError.BadRequest,
                    "Refresh token is invalid/expired.");
            
            var (refreshToken, refreshTokenExpiration) = await _tokenProvider.GenerateRefreshToken(user, existingRefreshToken.Token);
            var (token, tokenExpiration) = await _tokenProvider.GenerateAccessToken(user);

            return Result<AuthResultViewModel>.Success(new AuthResultViewModel
            {
                AccessToken = token,
                AccessTokenExpires = tokenExpiration,
                RefreshToken = refreshToken,
                RefreshTokenExpires = refreshTokenExpiration
            });
        }
    }
}