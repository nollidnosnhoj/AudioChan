﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Audiochan.Core.Common.Constants;
using Audiochan.Core.Common.Interfaces;
using Audiochan.Core.Common.Models;
using Audiochan.Core.Common.Settings;
using Audiochan.Core.Features.Audios.GetAudio;
using Audiochan.Core.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;

namespace Audiochan.Core.Features.Audios.UpdatePicture
{
    public class UpdateAudioPictureCommand : IRequest<Result<ImageUploadResponse>>
    {
        public Guid AudioId { get; set; }
        public string Data { get; set; } = string.Empty;
    }

    public class UpdateAudioCommandHandler : IRequestHandler<UpdateAudioPictureCommand, Result<ImageUploadResponse>>
    {
        private readonly MediaStorageSettings _storageSettings;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IStorageService _storageService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IImageProcessingService _imageProcessingService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public UpdateAudioCommandHandler(IOptions<MediaStorageSettings> options,
            IStorageService storageService,
            ICurrentUserService currentUserService,
            IImageProcessingService imageProcessingService,
            IDateTimeProvider dateTimeProvider, 
            IUnitOfWork unitOfWork, 
            ICacheService cacheService, IMapper mapper)
        {
            _storageSettings = options.Value;
            _storageService = storageService;
            _currentUserService = currentUserService;
            _imageProcessingService = imageProcessingService;
            _dateTimeProvider = dateTimeProvider;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
            _mapper = mapper;
        }

        public async Task<Result<ImageUploadResponse>> Handle(UpdateAudioPictureCommand command,
            CancellationToken cancellationToken)
        {
            var container = string.Join('/', _storageSettings.Image.Container, "audios");
            var currentUserId = _currentUserService.GetUserId();

            var audio = await _unitOfWork.Audios
                .LoadForUpdate(command.AudioId, cancellationToken);
        
            if (audio == null) 
                return Result<ImageUploadResponse>.Fail(ResultError.NotFound);
        
            if (audio.UserId != currentUserId)
                return Result<ImageUploadResponse>.Fail(ResultError.Forbidden);
            
            var blobName = $"{audio.Id}/{_dateTimeProvider.Now:yyyyMMddHHmmss}.jpg";
            
            _unitOfWork.BeginTransaction();
            try
            {

                await _imageProcessingService.UploadImage(command.Data, container, blobName, cancellationToken);

                if (!string.IsNullOrEmpty(audio.Picture))
                    await _storageService.RemoveAsync(_storageSettings.Image.Bucket, container, audio.Picture,
                        cancellationToken);

                audio.Picture = blobName;
                _unitOfWork.Audios.Update(audio);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _cacheService.RemoveAsync(new GetAudioCacheOptions(command.AudioId), cancellationToken);
            }
            catch
            {
                _unitOfWork.RollbackTransaction();
                throw;
            }

            await _unitOfWork.CommitTransactionAsync();
            return Result<ImageUploadResponse>.Success(new ImageUploadResponse
            {
                Url = string.Format(MediaLinkInvariants.AudioPictureUrl, blobName)
            });
        }
    }
}