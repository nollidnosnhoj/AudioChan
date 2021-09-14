﻿using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using Audiochan.Core.Common.Interfaces.Persistence;
using Audiochan.Core.Common.Interfaces.Services;
using Audiochan.Domain.Entities;
using MediatR;

namespace Audiochan.Core.Audios.GetAudio
{
    public record GetAudioQuery(long Id) : IRequest<AudioDto?>
    {
    }

    public sealed class GetAudioSpecification : Specification<Audio>
    {
        public GetAudioSpecification(long id)
        {
            Query.AsNoTracking();
            Query.Where(x => x.Id == id);
        }
    }

    public class GetAudioQueryHandler : IRequestHandler<GetAudioQuery, AudioDto?>
    {
        private readonly ICacheService _cacheService;
        private readonly IUnitOfWork _unitOfWork;

        public GetAudioQueryHandler(ICacheService cacheService, IUnitOfWork unitOfWork)
        {
            _cacheService = cacheService;
            _unitOfWork = unitOfWork;
        }

        public async Task<AudioDto?> Handle(GetAudioQuery query, CancellationToken cancellationToken)
        {
            var cacheOptions = new GetAudioCacheOptions(query.Id);
            
            var audio = await _cacheService
                .GetAsync<AudioDto>(cacheOptions, cancellationToken);

            if (audio is not null) return audio;

            audio = await _unitOfWork.Audios.GetFirstAsync<AudioDto>(new GetAudioSpecification(query.Id), cancellationToken);

            if (audio is null) return null;
            
            await _cacheService.SetAsync(audio, cacheOptions, cancellationToken);

            return audio;
        }
    }
}