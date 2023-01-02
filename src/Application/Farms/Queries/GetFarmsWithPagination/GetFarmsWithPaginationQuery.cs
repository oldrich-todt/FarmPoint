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
using FarmPoint.Domain.Common;
using FarmPoint.Domain.Entities;
using FarmPoint.Domain.Specifications;
using MediatR;

namespace FarmPoint.Application.Farms.Queries.GetFarmsWithPagination;
public record GetFarmsWithPaginationQuery: IRequest<PaginatedList<FarmDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class GetFarmsWithPaginationQueryHandler : IRequestHandler<GetFarmsWithPaginationQuery, PaginatedList<FarmDto>>
{
    private readonly IRepository<Farm> _farmRepository;
    private readonly IMapper _mapper;

    public GetFarmsWithPaginationQueryHandler(IRepository<Farm> farmRepository, IMapper mapper)
    {
        _farmRepository = farmRepository ?? throw new ArgumentNullException(nameof(farmRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<PaginatedList<FarmDto>> Handle(GetFarmsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var count = await _farmRepository.CountAsync(cancellationToken).ConfigureAwait(false);

        var pagedSpec = new FarmPaginatedSpecification(request.PageNumber * request.PageSize, request.PageSize);

        var farms = await _farmRepository.ListAsync(pagedSpec, cancellationToken).ConfigureAwait(false);

        var farmDtos = _mapper.Map<List<FarmDto>>(farms);

        return PaginatedList<FarmDto>.Create(farmDtos, request.PageNumber, request.PageSize, count);
    }
}
