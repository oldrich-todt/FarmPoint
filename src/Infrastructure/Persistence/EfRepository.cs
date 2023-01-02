using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmPoint.Domain.Common;
using Ardalis.Specification.EntityFrameworkCore;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;

namespace FarmPoint.Infrastructure.Persistence;
public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
