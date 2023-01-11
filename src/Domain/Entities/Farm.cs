using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmPoint.Domain.Entities;
public class Farm: BaseAuditableEntity, IAggregateRoot
{
    public required string Name { get; set; }
    public required string OwnerId { get; set; }

    public static Farm Create(string name, string ownerId)
    {
        return new Farm { Name = name, OwnerId = ownerId };
    }
}
