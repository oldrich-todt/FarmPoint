using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FarmPoint.Application.Farms.EventHandlers;
using FarmPoint.Domain.Common;
using FarmPoint.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace FarmPoint.Application.IntegrationTests.Farms.EventHandlers;

using static Testing;

public class FarmCreatedEventTests: BaseTestFixture
{
    [Test]
    [TestCase(typeof(ILogger<FarmCreatedEventHandler>))]
    public void ShouldBeRegistered(Type type)
    {
        var service = GetService(type);

        service.Should().NotBeNull();
        service.Should().BeAssignableTo(type);
    }
}
