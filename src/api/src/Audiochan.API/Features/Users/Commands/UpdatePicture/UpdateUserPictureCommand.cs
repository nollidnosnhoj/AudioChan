﻿using System.Threading;
using System.Threading.Tasks;
using Audiochan.Core.CQRS;
using Audiochan.Core.Dtos.Responses;
using Audiochan.Core.Extensions;
using Audiochan.Core.Interfaces;
using Audiochan.Core.Persistence;
using Audiochan.Core.Services;
using MediatR;

namespace Audiochan.Core.Users.Commands
{
    public record UpdateUserPictureCommand(long UserId, string Data = "") : IImageData,
        ICommandRequest<Result<ImageUploadResponse>>;

    public class UpdateUserPictureCommandHandler : IRequestHandler<UpdateUserPictureCommand, Result<ImageUploadResponse>>
    {
        private readonly IImageService _imageService;
        private readonly long _currentUserId;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRandomIdGenerator _randomIdGenerator;

        public UpdateUserPictureCommandHandler(IImageService imageService,
            ICurrentUserService currentUserService, 
            IUnitOfWork unitOfWork, 
            IRandomIdGenerator randomIdGenerator)
        {
            _imageService = imageService;
            currentUserService.User.TryGetUserId(out _currentUserId);
            _unitOfWork = unitOfWork;
            _randomIdGenerator = randomIdGenerator;
        }

        public async Task<Result<ImageUploadResponse>> Handle(UpdateUserPictureCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.FindAsync(command.UserId, cancellationToken);

            if (user!.Id != _currentUserId)
                return Result<ImageUploadResponse>.Forbidden();
        
            var blobName = string.Empty;
            if (string.IsNullOrEmpty(command.Data))
            {
                await RemoveOriginalPicture(user.Picture, cancellationToken);
                user.Picture = null;
            }
            else
            {
                blobName = $"{await _randomIdGenerator.GenerateAsync(size: 15)}.jpg";
                await _imageService.UploadImage(command.Data, AssetContainerConstants.USER_PICTURES, blobName, cancellationToken);
                await RemoveOriginalPicture(user.Picture, cancellationToken);
                user.Picture = blobName;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<ImageUploadResponse>.Success(new ImageUploadResponse
            {
                Url = MediaLinkConstants.USER_PICTURE + blobName
            });
        }
        
        private async Task RemoveOriginalPicture(string? picture, CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrEmpty(picture))
            {
                await _imageService.RemoveImage(AssetContainerConstants.USER_PICTURES, picture, cancellationToken);
            }
        }
    }
}