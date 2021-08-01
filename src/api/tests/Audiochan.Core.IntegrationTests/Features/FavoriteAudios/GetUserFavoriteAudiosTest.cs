﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Audiochan.Core.Entities;
using Audiochan.Core.Entities.Enums;
using Audiochan.Core.Features.Users.GetUserFavoriteAudios;
using Audiochan.Tests.Common.Fakers.Audios;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Audiochan.Core.IntegrationTests.Features.FavoriteAudios
{
    public class GetUserFavoriteAudiosTest : TestBase
    {
        private readonly Faker _faker;

        public GetUserFavoriteAudiosTest(TestFixture testFixture) : base(testFixture)
        {
            _faker = new Faker();
        }

        [Fact]
        public async Task ShouldReturnFavoriteAudioSuccessfully()
        {
            // Assign
            var (targetId, _) = await RunAsAdministratorAsync();
            var audioFaker = new AudioFaker(targetId);
            var audios = audioFaker
                .SetFixedVisibility(Visibility.Public)
                .Generate(3);
            Insert(audios.ToArray());
            var (observerId, observerUsername) = await RunAsUserAsync(
                _faker.Random.String2(15), 
                _faker.Internet.Password(), 
                Array.Empty<string>());
            var favoriteAudios = new List<FavoriteAudio>();
            foreach (var audio in audios)
            {
                var favoriteAudio = new FavoriteAudio
                {
                    AudioId = audio.Id,
                    UserId = observerId,
                    FavoriteDate = new DateTime(2020, 12, 25)
                };
                
                favoriteAudios.Add(favoriteAudio);
            }
            Insert(favoriteAudios.ToArray());

            // Act
            var response = await SendAsync(new GetUserFavoriteAudiosQuery
            {
                Username = observerUsername
            });

            // Assert
            response.Should().NotBeNull();
            response.Count.Should().Be(3);
        }
    }
}