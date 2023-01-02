using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmPoint.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmPoint.Application.Common.Mappings;

public static class MappingExtensions
{
    public static PaginatedList<TDestination> PaginatedList<TDestination>(
        this List<TDestination> entities, 
        int pageNumber, 
        int pageSize,
        int count
        ) where TDestination : class
        => Models.PaginatedList<TDestination>.Create(entities, pageNumber, pageSize, count);
}
