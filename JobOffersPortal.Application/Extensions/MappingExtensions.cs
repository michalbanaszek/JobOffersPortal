﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Extensions
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize, IUriService uriService)
            => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize, uriService);

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
            => queryable.ProjectTo<TDestination>(configuration).ToListAsync();
    }
}
