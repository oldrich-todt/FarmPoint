using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace FarmPoint.Domain.Common;
public interface IRepository<T>: IRepositoryBase<T> where T : class, IAggregateRoot
{
}
