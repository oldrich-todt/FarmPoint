using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmPoint.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FarmPoint.Application.Farms.Commands.CreateFarm;
public class CreateFarmCommandValidator: AbstractValidator<CreateFarmCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateFarmCommandValidator(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name must not be empty.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters")
            .MustAsync(BeUniqueName).WithMessage("The farm with this name already exists");
    }

    private Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return _context.Farms.AllAsync(f => f.Name != name, cancellationToken);
    }
}
