using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FarmPoint.Application.Common.Interfaces;
using FarmPoint.Domain.Entities;
using FarmPoint.Domain.Events;
using MediatR;

namespace FarmPoint.Application.Farms.Commands.CreateFarm;
public record CreateFarmCommand: IRequest<FarmDto>
{
    public required string Name { get; set; }
}

public class CreateFarmCommandHandler: IRequestHandler<CreateFarmCommand, FarmDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateFarmCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<FarmDto> Handle(CreateFarmCommand command, CancellationToken cancellationToken)
    {
        var entity = new Farm
        {
            Name = command.Name,
        };

        entity.AddDomainEvent(new FarmCreatedEvent(entity));

        var entityEntry = _context.Farms.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<FarmDto>(entityEntry.Entity);
    }
}
