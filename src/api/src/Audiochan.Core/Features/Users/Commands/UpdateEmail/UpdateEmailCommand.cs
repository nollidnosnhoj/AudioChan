﻿using System.Threading;
using System.Threading.Tasks;
using Audiochan.Core.Commons;
using Audiochan.Core.Commons.CQRS;
using Audiochan.Core.Commons.Extensions;

using Audiochan.Core.Commons.Services;
using Audiochan.Core.Persistence;
using MediatR;

namespace Audiochan.Core.Features.Users.Commands.UpdateEmail
{
    public record UpdateEmailCommand : ICommandRequest<Result>
    {
        public long UserId { get; init; }
        public string NewEmail { get; init; } = null!;

        public static UpdateEmailCommand FromRequest(long userId, UpdateEmailRequest request) => new()
        {
            UserId = userId,
            NewEmail = request.NewEmail
        };
    }

    public class UpdateEmailCommandHandler : IRequestHandler<UpdateEmailCommand, Result>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmailCommandHandler(ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateEmailCommand command, CancellationToken cancellationToken)
        {
            _currentUserService.User.TryGetUserId(out var currentUserId);

            var user = await _unitOfWork.Users.FindAsync(command.UserId, cancellationToken);
            if (user!.Id != currentUserId) return Result.Forbidden();

            user.Email = command.NewEmail;
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}