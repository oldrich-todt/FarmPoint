using FarmPoint.Application.Common.Models;
using FarmPoint.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using GetFarmsWithPagination = FarmPoint.Application.Farms.Queries.GetFarmsWithPagination;
using CreateFarm = FarmPoint.Application.Farms.Commands.CreateFarm;

namespace WebUI.Controllers;

public class FarmController: ApiControllerBase
{
    public async Task<PaginatedList<GetFarmsWithPagination.FarmDto>> GetFarmsWithPagination([FromQuery] GetFarmsWithPagination.GetFarmsWithPaginationQuery query)
        => await Mediator.Send(query);

    public async Task<CreateFarm.FarmDto> CreateFarm([FromBody] CreateFarm.CreateFarmCommand command)
        => await Mediator.Send(command);
}
