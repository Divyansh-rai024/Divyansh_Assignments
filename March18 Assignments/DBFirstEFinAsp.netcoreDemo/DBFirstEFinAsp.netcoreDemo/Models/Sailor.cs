using System;
using System.Collections.Generic;

namespace DBFirstEFinAsp.netcoreDemo.Models;

public partial class Sailor
{
    public int Sid { get; set; }

    public string? Sname { get; set; }

    public int? Rating { get; set; }

    public int? Age { get; set; }
}
