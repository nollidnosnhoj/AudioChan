﻿namespace Audiochan.Core.Models.ViewModels
{
    public record AuthResultViewModel
    {
        public string AccessToken { get; init; }
        public long AccessTokenExpires { get; init; }
        public string RefreshToken { get; init; }
        public long RefreshTokenExpires { get; init; }
    }
}