using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmPoint.Domain.Entities;
public class Farm: BaseAuditableEntity
{
    public required string Name { get; set; }
    public required string OwnerId { get; set; }
}
