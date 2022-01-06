﻿using System.Threading;
using System.Threading.Tasks;
using Audiochan.Application.Commons.Extensions;
using Audiochan.Application.Features.Audios.Commands.SetFavoriteAudio;
using Audiochan.Application.Features.Users.Queries.CheckIfAudioFavorited;
using Audiochan.Application.Features.Users.Queries.GetUserFavoriteAudios;
using Audiochan.Application.Services;
using Audiochan.Server.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Audiochan.API.Controllers.Me
{
    [Area("me")]
    [Authorize]
    [Route("[area]/favorites/audios")]
    [ProducesResponseType(401)]
    public class FavoriteAudiosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly long _currentUserId;
        private readonly string _currentUsername;

        public FavoriteAudiosController(ICurrentUserService currentUserService, IMediator mediator)
        {
            _mediator = mediator;
            currentUserService.User.TryGetUserId(out _currentUserId);
            currentUserService.User.TryGetUserName(out _currentUsername);
        }
        
        [HttpGet(Name = "YourFavoriteAudios")]
        [SwaggerOperation(
            Summary = "Get Your favorite audios",
            Description = "Requires authentication.",
            OperationId = "YourFavoriteAudios",
            Tags = new[] {"me"}
        )]
        public async Task<IActionResult> GetYourFavoriteAudios([FromQuery] OffsetPaginationQueryParams queryParams,
            CancellationToken cancellationToken = default)
        {
            var query = new GetUserFavoriteAudiosQuery
            {
                Username = _currentUsername,
                Offset = queryParams.Offset,
                Size = queryParams.Size
            };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        
        [HttpHead("{audioId:long}", Name="CheckIfUserFavoritedAudio")]
        [SwaggerOperation(
            Summary = "Check if the authenticated user favorited an audio",
            Description = "Requires authentication.",
            OperationId = "CheckIfUserFavoritedAudio",
            Tags = new[] {"me"}
        )]
        public async Task<IActionResult> IsFavoriteAudio(long audioId, CancellationToken cancellationToken)
        {
            var command = new CheckIfAudioFavoritedQuery(audioId, _currentUserId);
            return await _mediator.Send(command, cancellationToken)
                ? Ok()
                : NotFound();
        }
        
        [HttpPut("{audioId:long}", Name = "FavoriteAudio")]
        [SwaggerOperation(
            Summary = "Favorite an audio",
            Description = "Requires authentication.",
            OperationId = "FavoriteAudio",
            Tags = new[] {"me"}
        )]
        public async Task<IActionResult> FavoriteAudio(long audioId, CancellationToken cancellationToken)
        {
            var command = new SetFavoriteAudioCommand(audioId, _currentUserId, true);
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpDelete("{audioId:long}", Name = "UnFavoriteAudio")]
        [SwaggerOperation(
            Summary = "UnFavorite an audio",
            Description = "Requires authentication.",
            OperationId = "UnFavoriteAudio",
            Tags = new[] {"me"}
        )]
        public async Task<IActionResult> UnFavoriteAudio(long audioId, CancellationToken cancellationToken)
        {
            var command = new SetFavoriteAudioCommand(audioId, _currentUserId, false);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}