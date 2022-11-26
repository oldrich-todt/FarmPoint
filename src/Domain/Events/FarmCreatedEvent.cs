using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmPoint.Domain.Events;
public class FarmCreatedEvent: BaseEvent
{
    public FarmCreatedEvent(Farm farm)
    {
        Farm = farm;
    }

    public Farm Farm { get; set; }
}
