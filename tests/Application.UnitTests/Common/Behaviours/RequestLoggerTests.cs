using FarmPoint.Application.Common.Behaviours;
using FarmPoint.Application.Common.Interfaces;
using FarmPoint.Application.Farms.Commands.CreateFarm;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace FarmPoint.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateFarmCommand>> _logger = null!;
    private Mock<ICurrentUserService> _currentUserService = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateFarmCommand>>();
        _currentUserService = new Mock<ICurrentUserService>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _currentUserService.Setup(x => x.UserId).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<CreateFarmCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        await requestLogger.Process(new CreateFarmCommand { Name = "Farm" }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateFarmCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        await requestLogger.Process(new CreateFarmCommand { Name = "Farm" }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
