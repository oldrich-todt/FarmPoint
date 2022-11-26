using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmPoint.Application.Farms.Queries.GetFarmsWithPagination;
using FluentAssertions;
using NUnit.Framework;

namespace FarmPoint.Application.IntegrationTests.Farms.Queries;

using static Testing;

public class GetFarmsWithPaginationTests: BaseTestFixture
{
    [Test]
    public async Task ShouldReturnPaginatedResult()
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
}
