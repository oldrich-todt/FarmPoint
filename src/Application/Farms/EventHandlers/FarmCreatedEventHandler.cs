using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmPoint.Domain.Entities;
using FarmPoint.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FarmPoint.Application.Farms.EventHandlers;
public class FarmCreatedEventHandler : INotificationHandler<FarmCreatedEvent>
{
    private readonly ILogger<FarmCreatedEventHandler> _logger;

    public FarmCreatedEventHandler(ILogger<FarmCreatedEventHandler> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task Handle(FarmCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Farm domain event: {eventType}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
