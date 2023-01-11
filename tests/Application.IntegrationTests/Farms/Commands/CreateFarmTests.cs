using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FarmPoint.Application.Common.Exceptions;
using FarmPoint.Application.Common.Interfaces;
using FarmPoint.Application.Farms.Commands.CreateFarm;
using FarmPoint.Domain.Common;
using FarmPoint.Domain.Entities;
using FluentAssertions;
using MediatR;
using NUnit.Framework;

namespace FarmPoint.Application.IntegrationTests.Farms.Commands;

using static Testing;

internal class CreateFarmTests: BaseTestFixture
{
    [Test]
    [TestCase(typeof(IRepository<Farm>))]
    [TestCase(typeof(IMapper))]
    [TestCase(typeof(ICurrentUserService))]
    public void ShouldBeRegistered(Type type)
    {
        var service = GetService(type);

        service.Should().NotBeNull();
        service.Should().BeAssignableTo(type);
    }

    [Test]
    public async Task ShouldCreateFarm()
    {
        await RunAsDefaultContributorUser();

        var command = new CreateFarmCommand
        {
            Name = "Farm",

        };

        var result = await SendAsync(command);

        result.Should().NotBeNull();
        result.Name.Should().Be("Farm");
    }

    [Test]
    public async Task ShouldThrowAuthorizationException()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreateFarmCommand
        {
            Name = "Farm"
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Test]
    public async Task ShouldThrowNameUniquenessValidationException()
    {
        var userId = await RunAsDefaultContributorUser();

        var farm1Command = new CreateFarmCommand
        {
            Name = "DuplicitFarm"
        };

        var farm1Result = await SendAsync(farm1Command);

        var farm2Command = new CreateFarmCommand
        {
            Name = "DuplicitFarm"
        };

        await FluentActions.Invoking(() => SendAsync(farm2Command))
            .Should()
            .ThrowAsync<ValidationException>()
            .WithMessage("One or more validation failures have occurred.");
    }
}
