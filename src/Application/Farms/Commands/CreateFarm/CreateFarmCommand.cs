﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FarmPoint.Application.Common.Interfaces;
using FarmPoint.Application.Common.Logging;
using FarmPoint.Application.Common.Security;
using FarmPoint.Domain.Common;
using FarmPoint.Domain.Entities;
using FarmPoint.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FarmPoint.Application.Farms.Commands.CreateFarm;

[Authorize(Roles = "contributor")]
public record CreateFarmCommand: IRequest<FarmDto>
{
    public required string Name { get; set; }
}

public class CreateFarmCommandHandler: IRequestHandler<CreateFarmCommand, FarmDto>
{
    private readonly IRepository<Farm> _farmRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<CreateFarmCommandHandler> _logger;

    public CreateFarmCommandHandler(IRepository<Farm> farmRepository, IMapper mapper, ICurrentUserService currentUser, ILogger<CreateFarmCommandHandler> logger)
    {
        _farmRepository = farmRepository ?? throw new ArgumentNullException(nameof(farmRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<FarmDto> Handle(CreateFarmCommand command, CancellationToken cancellationToken)
    {
        var entity = Farm.Create(command.Name, _currentUser.UserId);
        entity.AddDomainEvent(new FarmCreatedEvent(entity));

        try
        {
            var farm = await _farmRepository.AddAsync(entity, cancellationToken);
            return _mapper.Map<FarmDto>(farm);
        }
        catch (Exception ex)
        {
            _logger.FarmCreatingError(ex);
            throw;
        }
    }
}
