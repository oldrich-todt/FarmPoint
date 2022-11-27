using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FarmPoint.Application.Common.Interfaces;
using FarmPoint.Application.Common.Security;
using FarmPoint.Domain.Entities;
using FarmPoint.Domain.Events;
using MediatR;

namespace FarmPoint.Application.Farms.Commands.CreateFarm;

[Authorize(Roles = "contributor")]
public record CreateFarmCommand: IRequest<FarmDto>
{
    public required string Name { get; set; }
}

public class CreateFarmCommandHandler: IRequestHandler<CreateFarmCommand, FarmDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;

    public CreateFarmCommandHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task<FarmDto> Handle(CreateFarmCommand command, CancellationToken cancellationToken)
    {
        var entity = new Farm
        {
            Name = command.Name,
            OwnerId = _currentUser.UserId
        };

        entity.AddDomainEvent(new FarmCreatedEvent(entity));

        var entityEntry = _context.Farms.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<FarmDto>(entityEntry.Entity);
    }
}
