﻿using System.Threading;
using System.Threading.Tasks;
using Audiochan.Core.Common.Models.Responses;
using Audiochan.Core.Features.Audios;
using Audiochan.Core.Features.Audios.CreateAudio;
using Audiochan.Core.Features.Audios.GetAudio;
using Audiochan.Core.Features.Audios.GetAudioList;
using Audiochan.Core.Features.Audios.GetRandomAudio;
using Audiochan.Core.Features.Audios.RemoveAudio;
using Audiochan.Core.Features.Audios.UpdateAudio;
using Audiochan.Core.Features.Audios.UpdatePicture;
using Audiochan.Extensions;
using Audiochan.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Audiochan.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AudiosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AudiosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetAudios")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagedList<AudioViewModel>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Returns a list of audios.", OperationId = "GetAudios", Tags = new[] {"audios"})]
        public async Task<IActionResult> GetList([FromQuery] GetAudioListRequest query, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(query, cancellationToken));
        }

        [HttpGet("{audioId}", Name = "GetAudio")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AudioDetailViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Return an audio by ID.", OperationId = "GetAudio", Tags = new[] {"audios"})]
        public async Task<IActionResult> Get(long audioId, [FromQuery] string privateKey, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAudioRequest(audioId, privateKey), cancellationToken);
            return result != null
                ? Ok(result)
                : NotFound(ErrorViewModel.NotFound("Audio was not found."));
        }
        
        [HttpGet("random", Name = "GetRandomAudio")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AudioDetailViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Return a random audio.", OperationId = "GetRandomAudio", Tags = new[] {"audios"})]
        public async Task<IActionResult> GetRandom(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetRandomAudioRequest(), cancellationToken);
            return result != null
                ? Ok(result)
                : NotFound(ErrorViewModel.NotFound("Audio was not found."));
        }

        [HttpPost(Name = "CreateAudio")]
        [ProducesResponseType(typeof(AudioDetailViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [SwaggerOperation(
            Summary = "Create audio.",
            Description = "Requires authentication.",
            OperationId = "CreateAudio",
            Tags = new[] {"audios"})]
        public async Task<IActionResult> Create([FromBody] CreateAudioRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.IsSuccess
                ? CreatedAtAction(nameof(Get), new {audioId = result.Data.Id}, result.Data)
                : result.ReturnErrorResponse();
        }

        [HttpPut("{audioId:long}", Name = "UpdateAudio")]
        [ProducesResponseType(typeof(AudioDetailViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [SwaggerOperation(
            Summary = "Update audio's details.",
            Description = "Requires authentication.",
            OperationId = "UpdateAudio",
            Tags = new[] {"audios"})]
        public async Task<IActionResult> Update(long audioId, 
            [FromBody] UpdateAudioRequest request,
            CancellationToken cancellationToken)
        {
            request.AudioId = audioId;
            var result = await _mediator.Send(request, cancellationToken);
            return result.IsSuccess ? Ok(result.Data) : result.ReturnErrorResponse();
        }

        [HttpDelete("{audioId:long}", Name = "DeleteAudio")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Remove audio.",
            Description = "Requires authentication.",
            OperationId = "DeleteAudio",
            Tags = new[] {"audios"})]
        public async Task<IActionResult> Destroy(long audioId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new RemoveAudioRequest(audioId), cancellationToken);
            return result.IsSuccess ? NoContent() : result.ReturnErrorResponse();
        }

        [HttpPatch("{audioId:long}/picture")]
        [SwaggerOperation(
            Summary = "Add Picture.",
            Description = "Requires authentication.",
            OperationId = "AddAudioPicture",
            Tags = new[] {"audios"})]
        public async Task<IActionResult> AddPicture(long audioId, 
            [FromBody] UpdateAudioPictureRequest request,
            CancellationToken cancellationToken)
        {
            request.AudioId = audioId;
            var result = await _mediator.Send(request, cancellationToken);
            return result.IsSuccess
                ? Ok(new {Image = result.Data})
                : result.ReturnErrorResponse();
        }
    }
}