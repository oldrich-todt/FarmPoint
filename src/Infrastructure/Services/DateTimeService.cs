using FarmPoint.Application.Common.Interfaces;

namespace FarmPoint.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
