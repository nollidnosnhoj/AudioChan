﻿using System.Threading;
using System.Threading.Tasks;
using Audiochan.Core.Commons;
using Audiochan.Core.Commons.CQRS;

using Audiochan.Core.Commons.Services;
using Audiochan.Core.Persistence;
using Audiochan.Domain.Entities;
using MediatR;

namespace Audiochan.Core.Features.Users.Commands.SetFavoriteAudio
{
    public record SetFavoriteAudioCommand(long AudioId, long UserId, bool IsFavoriting) : ICommandRequest<Result>
    {
    }

    public class SetFavoriteAudioCommandHandler : IRequestHandler<SetFavoriteAudioCommand, Result>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public SetFavoriteAudioCommandHandler(IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result> Handle(SetFavoriteAudioCommand command, CancellationToken cancellationToken)
        {
            var audio = await _unitOfWork.Audios
                .LoadAudioWithFavorites(command.AudioId, command.UserId, cancellationToken);

            if (audio == null)
                return Result.NotFound<Audio>();

            if (command.IsFavoriting)
                audio.Favorite(command.UserId, _dateTimeProvider.Now);
            else
                audio.UnFavorite(command.UserId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}