using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmPoint.Application.Common.Mappings;
using FarmPoint.Domain.Entities;

namespace FarmPoint.Application.Farms.Queries.GetFarmsWithPagination;
public class FarmDto: IMapFrom<Farm>
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
