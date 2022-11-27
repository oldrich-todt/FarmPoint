using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmPoint.Application.Common.Exceptions;
using FarmPoint.Application.Farms.Commands.CreateFarm;
using FarmPoint.Application.Farms.Queries.GetFarmsWithPagination;
using FluentAssertions;
using NUnit.Framework;

namespace FarmPoint.Application.IntegrationTests.Farms.Queries;

using static Testing;

public class GetFarmsWithPaginationTests: BaseTestFixture
{
    [Test]
    public async Task ShouldReturnEmptyPaginatedResult()
    {
        await RunAsDefaultUserAsync();

        var query = new GetFarmsWithPaginationQuery()
        {
            PageNumber = 1,
            PageSize = 1
        };

        var result = await SendAsync(query);

        result.HasPreviousPage.Should().BeFalse();
        result.Items.Count.Should().Be(0);
        result.TotalCount.Should().Be(0);
    }

    [Test]
    public async Task ShouldReturnPaginatedResult()
    {
        await RunAsDefaultContributorUser();

        var farmCommand1 = new CreateFarmCommand
        {
            Name = "Farm1"
        };

        var farm1 = await SendAsync(farmCommand1);

        var farmCommand2 = new CreateFarmCommand
        {
            Name = "Farm2"
        };

        await SendAsync(farmCommand2);

        await RunAsDefaultUserAsync();

        var query = new GetFarmsWithPaginationQuery()
        {
            PageNumber = 1,
            PageSize = 1
        };

        var result = await SendAsync(query);

        result.HasPreviousPage.Should().BeFalse();
        result.HasNextPage.Should().BeTrue();
        result.Items.Count.Should().Be(1);
        result.TotalCount.Should().Be(2);
        result.TotalPages.Should().Be(2);
        result.Items[0].Id.Should().Be(farm1.Id);
    }

    [Test]
    public async Task ShouldThrowPageNumberAtLeastOneValidationException()
    {
        await RunAsDefaultUserAsync();

        var query = new GetFarmsWithPaginationQuery
        {
            PageNumber = 0,
            PageSize = 1
        };

        await FluentActions.Invoking(() => SendAsync(query))
            .Should()
            .ThrowAsync<ValidationException>()
            .WithMessage("One or more validation failures have occurred.");
    }

    [Test]
    public async Task ShouldThrowPageSizeAtLeastOneValidationException()
    {
        await RunAsDefaultUserAsync();

        var query = new GetFarmsWithPaginationQuery
        {
            PageNumber = 1,
            PageSize = 0
        };

        await FluentActions.Invoking(() => SendAsync(query))
            .Should()
            .ThrowAsync<ValidationException>()
            .WithMessage("One or more validation failures have occurred.");
    }
}
