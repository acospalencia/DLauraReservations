using System;
using System.Collections.Generic;

namespace DLaura.Entities;

public partial class Location
{
    public int Id { get; set; }

    public int TableNumber { get; set; }

    public string CoordinateX { get; set; } = null!;

    public string CoordinateY { get; set; } = null!;

    public virtual UserTable TableNumberNavigation { get; set; } = null!;
}
