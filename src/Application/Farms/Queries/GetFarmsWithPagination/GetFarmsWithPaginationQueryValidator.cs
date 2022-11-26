using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace FarmPoint.Application.Farms.Queries.GetFarmsWithPagination;
public class GetFarmsWithPaginationQueryValidator: AbstractValidator<GetFarmsWithPaginationQuery>
{
    public GetFarmsWithPaginationQueryValidator()
    {
        RuleFor(q => q.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(q => q.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageSize at least greater than or equal to 1.");
    }
}
