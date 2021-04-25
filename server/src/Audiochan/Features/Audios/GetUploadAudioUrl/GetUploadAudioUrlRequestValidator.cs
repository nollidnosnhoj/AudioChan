﻿using Audiochan.Core.Extensions;
using Audiochan.Core.Models.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Audiochan.Features.Audios.GetUploadAudioUrl
{
    public class GetUploadAudioUrlRequestValidator : AbstractValidator<GetUploadAudioUrlRequest>
    {
        public GetUploadAudioUrlRequestValidator(IOptions<MediaStorageSettings> options)
        {
            RuleFor(x => x.FileName)
                .FileNameValidation(options.Value.Audio.ValidContentTypes);
            RuleFor(x => x.FileSize)
                .FileSizeValidation(options.Value.Audio.MaximumFileSize);
        }
    }
}