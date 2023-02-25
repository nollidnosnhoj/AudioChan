﻿using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Audiochan.Common.Exceptions;
using Audiochan.Common.Mediatr;
using Audiochan.Core.Features.Users.Models;
using Audiochan.Core.Persistence;
using MediatR;

namespace Audiochan.Core.Features.Users.Commands
{
    public class UpdateProfileCommand : AuthCommandRequest<UserViewModel>
    {
        public string? DisplayName { get; }

        public UpdateProfileCommand(string? displayName, ClaimsPrincipal user) : base(user)
        {
            DisplayName = displayName;
        }
    }

    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, UserViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProfileCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserViewModel> Handle(UpdateProfileCommand command, CancellationToken cancellationToken)
        {
            var userId = command.GetUserId();
            
            var user = await _unitOfWork.Users.FindAsync(userId, cancellationToken);
            
            if (user is null)
            {
                throw new UnauthorizedException();
            }

            if (user.Id != userId)
            {
                throw new UnauthorizedException();
            }
            
            // TODO: Update user stuff

            return new UserViewModel
            {
                Id = userId,
                Picture = user.ImageId,
                UserName = user.UserName
            };
        }
    }
}