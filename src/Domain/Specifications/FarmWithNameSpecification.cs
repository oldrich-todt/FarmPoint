using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace FarmPoint.Domain.Specifications;
public class FarmWithNameSpecification: Specification<Farm>
{
    public FarmWithNameSpecification(string name)
    {
        Query.Where(f => f.Name == name);
    }
}
