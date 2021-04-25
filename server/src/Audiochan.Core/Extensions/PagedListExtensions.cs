﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Audiochan.Core.Models.Interfaces;
using Audiochan.Core.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace Audiochan.Core.Extensions
{
    public static class PagedListExtensions
    {
        private const int DefaultPageNumber = 1;
        private const int DefaultPageSize = 30;
        
        public static async Task<PagedList<TResponse>> PaginateAsync<TResponse>(
            this IQueryable<TResponse> queryable,
            int page = DefaultPageNumber,
            int limit = DefaultPageSize,
            CancellationToken cancellationToken = default)
        {
            if (page == default) page = DefaultPageNumber;
            if (limit == default) limit = DefaultPageSize;
            var count = await queryable.CountAsync(cancellationToken);
            var pageNumber = Math.Max(DefaultPageNumber, page);
            var pageLimit = Math.Max(0, Math.Min(limit, DefaultPageSize));
            var list = await queryable
                .Skip((pageNumber - 1) * pageLimit)
                .Take(pageLimit)
                .ToListAsync(cancellationToken);
            return new PagedList<TResponse>(list, count, page, limit);
        }

        public static async Task<PagedList<TResponse>> PaginateAsync<TResponse>(this IQueryable<TResponse> queryable
            , IHasPage paginationQuery
            , CancellationToken cancellationToken = default)
        {
            return await queryable.PaginateAsync(paginationQuery.Page, paginationQuery.Size, cancellationToken);
        }

        public static async Task<List<TResponse>> CursorPaginateAsync<TResponse, TCursor>(
            this IQueryable<TResponse> queryable,
            IHasCursor<TCursor> request,
            Expression<Func<TResponse, TCursor>> property,
            Expression<Func<TResponse, bool>> predicate,
            CancellationToken cancellationToken = default) where TCursor : struct
        {
            queryable = queryable.OrderByDescending(property);

            if (request.Cursor.HasValue)
                queryable = queryable.Where(predicate);

            return await queryable.Take(request.Size).ToListAsync(cancellationToken);
        }
    }
}