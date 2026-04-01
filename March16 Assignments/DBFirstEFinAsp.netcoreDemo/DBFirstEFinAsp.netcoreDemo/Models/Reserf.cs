using System;
using System.Collections.Generic;

namespace DBFirstEFinAsp.netcoreDemo.Models;

public partial class Reserf
{
    public int? Sid { get; set; }

    public int? Bid { get; set; }

    public DateOnly? Day { get; set; }

    public virtual Boat? BidNavigation { get; set; }

    public virtual Sailor? SidNavigation { get; set; }
}
