﻿using System.Collections.Generic;

namespace Audiochan.Core.Features.Audios.Commands.UpdateAudio
{
    public class UpdateAudioRequest
    {
        public string? Title { get; init; }
        public string? Description { get; init; }
        public List<string>? Tags { get; init; }
    }
}