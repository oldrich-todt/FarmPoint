using FarmPoint.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FarmPoint.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Farm> Farms { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
