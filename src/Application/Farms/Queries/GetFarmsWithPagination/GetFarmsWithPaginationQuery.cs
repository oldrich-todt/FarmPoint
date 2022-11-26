using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmPoint.Application.Common.Interfaces;
using FarmPoint.Application.Common.Mappings;
using FarmPoint.Application.Common.Models;
using MediatR;

namespace FarmPoint.Application.Farms.Queries.GetFarmsWithPagination;
public record GetFarmsWithPaginationQuery: IRequest<PaginatedList<FarmDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class GetFarmsWithPaginationQueryHandler : IRequestHandler<GetFarmsWithPaginationQuery, PaginatedList<FarmDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetFarmsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<PaginatedList<FarmDto>> Handle(GetFarmsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Farms
            .OrderBy(f => f.Name)
            .ProjectTo<FarmDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
