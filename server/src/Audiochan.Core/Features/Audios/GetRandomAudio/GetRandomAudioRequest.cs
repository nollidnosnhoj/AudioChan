﻿using Audiochan.Core.Common.Models.Responses;
using Audiochan.Core.Features.Audios.GetAudio;
using MediatR;

namespace Audiochan.Core.Features.Audios.GetRandomAudio
{
    public record GetRandomAudioRequest : IRequest<Result<AudioDetailViewModel>>
    {
    }
}