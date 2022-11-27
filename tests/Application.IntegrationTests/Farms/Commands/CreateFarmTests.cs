using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmPoint.Application.Common.Exceptions;
using FarmPoint.Application.Farms.Commands.CreateFarm;
using FluentAssertions;
using NUnit.Framework;

namespace FarmPoint.Application.IntegrationTests.Farms.Commands;

using static Testing;

internal class CreateFarmTests: BaseTestFixture
{
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
