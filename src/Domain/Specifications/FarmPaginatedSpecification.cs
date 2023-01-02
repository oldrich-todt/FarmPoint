using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace FarmPoint.Domain.Specifications;
public class FarmPaginatedSpecification: Specification<Farm>
{
    public FarmPaginatedSpecification(int skip, int take)
    {
        if(take == 0)
        {
            take = int.MaxValue;
        }

        Query.Skip(skip).Take(take);
    }
}
