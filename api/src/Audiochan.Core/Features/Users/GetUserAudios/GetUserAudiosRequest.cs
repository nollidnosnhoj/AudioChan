﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Audiochan.Core.Common.Extensions;
using Audiochan.Core.Common.Interfaces;
using Audiochan.Core.Common.Mappings;
using Audiochan.Core.Common.Models;
using Audiochan.Core.Features.Audios.GetAudioList;
using Audiochan.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Audiochan.Core.Features.Users.GetUserAudios
{
    public class GetUserAudiosRequest : IHasPage, IRequest<PagedListDto<AudioViewModel>>
    {
        public string? Username { get; set; }
        public int Page { get; init; } = 1;
        public int Size { get; init; } = 30;
    }

    public class GetUserAudiosRequestHandler : IRequestHandler<GetUserAudiosRequest, PagedListDto<AudioViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public GetUserAudiosRequestHandler(ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedListDto<AudioViewModel>> Handle(GetUserAudiosRequest request,
            CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetUserId();
            var queryable = _unitOfWork.Audios.AsNoTracking()
                .Include(a => a.User)
                .Where(a => request.Username == a.User.UserName.ToLower());
            
            queryable = !string.IsNullOrEmpty(currentUserId) 
                ? queryable.Where(a => a.IsPublic || a.UserId == currentUserId) 
                : queryable.Where(a => a.IsPublic);

            return await queryable.Select(AudioMappings.AudioToListProjection())
                .PaginateAsync(request, cancellationToken);
        }
    }
}