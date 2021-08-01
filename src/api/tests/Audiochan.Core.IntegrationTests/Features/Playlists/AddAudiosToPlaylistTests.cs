﻿using System.Linq;
using System.Threading.Tasks;
using Audiochan.Core.Common.Models;
using Audiochan.Core.Entities.Enums;
using Audiochan.Core.Features.Playlists.AddAudiosToPlaylist;
using Audiochan.Tests.Common.Fakers.Audios;
using Audiochan.Tests.Common.Fakers.Playlists;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Audiochan.Core.IntegrationTests.Features.Playlists
{
    public class AddAudiosToPlaylistTests : TestBase
    {
        public AddAudiosToPlaylistTests(TestFixture testFixture) : base(testFixture)
        {
        }

        [Fact]
        public async Task ShouldSuccessfullyAddAudiosToPlaylist()
        {
            var (userId, _) = await RunAsUserAsync("kopacetic");
            var playlistFaker = new PlaylistFaker(userId);
            var playlist = playlistFaker.Generate();
            Insert(playlist);

            var audioFaker = new AudioFaker(userId);
            var audios = audioFaker
                .SetFixedVisibility(Visibility.Public)
                .Generate(3);
            InsertRange(audios);
            var audioIds = audios.Select(a => a.Id).ToList();

            var request = new AddAudiosToPlaylistCommand(playlist.Id, audioIds);
            var result = await SendAsync(request);
            var loadedPlaylist = ExecuteDbContext(db =>
            {
                return db.Playlists
                    .Include(a => a.Audios)
                    .SingleOrDefault(p => p.Id == playlist.Id);
            });

            result.IsSuccess.Should().BeTrue();
            loadedPlaylist.Should().NotBeNull();
            loadedPlaylist!.Audios.Count.Should().Be(audioIds.Count);
            loadedPlaylist.Audios.Should().Contain(a => audioIds.Contains(a.AudioId));
        }
        
        [Fact]
        public async Task ShouldNotAddAudios_WhenPlaylistDoesNotBelongToUser()
        {
            var (userId, _) = await RunAsUserAsync("kopacetic");
            var playlistFaker = new PlaylistFaker(userId);
            var playlist = playlistFaker.Generate();
            Insert(playlist);

            var (otherUserId, _) = await RunAsDefaultUserAsync();
            var audioFaker = new AudioFaker(otherUserId);
            var audios = audioFaker.Generate(3);
            var audioIds = audios.Select(a => a.Id).ToList();
            InsertRange(audios);

            var request = new AddAudiosToPlaylistCommand(playlist.Id, audioIds);
            var result = await SendAsync(request);
            var loadedPlaylist = ExecuteDbContext(db =>
            {
                return db.Playlists
                    .Include(a => a.Audios)
                    .SingleOrDefault(p => p.Id == playlist.Id);
            });

            result.IsSuccess.Should().BeFalse();
            result.ErrorCode.Should().Be(ResultError.Forbidden);
            loadedPlaylist.Should().NotBeNull();
            loadedPlaylist!.Audios.Count.Should().Be(0);
            loadedPlaylist.Audios.Should().NotContain(a => audioIds.Contains(a.AudioId));
        }

        [Fact]
        public async Task ShouldNotAddAudios_WhenAudiosDoesNotExistOrPrivate()
        {
            var (userId, _) = await RunAsUserAsync("kopacetic");
            var playlistFaker = new PlaylistFaker(userId);
            var playlist = playlistFaker.Generate();
            Insert(playlist);
            
            var (otherUserId, _) = await RunAsDefaultUserAsync();
            var audioFaker = new AudioFaker(otherUserId);
            var audios = audioFaker.Generate(2);
            audios[0].Visibility = Visibility.Unlisted;
            audios[1].Visibility = Visibility.Private;
            InsertRange(audios);
            var audioIds = audios.Select(x => x.Id).ToList();

            var request = new AddAudiosToPlaylistCommand(playlist.Id, audioIds);
            var result = await SendAsync(request);

            result.IsSuccess.Should().BeFalse();
        }
    }
}