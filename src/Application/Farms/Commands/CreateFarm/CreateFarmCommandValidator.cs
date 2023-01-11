using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmPoint.Application.Common.Interfaces;
using FarmPoint.Domain.Common;
using FarmPoint.Domain.Entities;
using FarmPoint.Domain.Specifications;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPoint.Application.Farms.Commands.CreateFarm;
public class CreateFarmCommandValidator: AbstractValidator<CreateFarmCommand>
{
    private readonly IRepository<Farm> _farmRepository;

    public CreateFarmCommandValidator(IRepository<Farm> farmRepository)
    {
        _farmRepository = farmRepository ?? throw new ArgumentNullException(nameof(farmRepository));

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name must not be empty.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters")
            .MustAsync(BeUniqueName).WithMessage("The farm with this name already exists");
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        var farmByNameSpec = new FarmWithNameSpecification(name);

        var anyFarmWithName = await _farmRepository.AnyAsync(farmByNameSpec, cancellationToken);

        return !anyFarmWithName;
    }
}
