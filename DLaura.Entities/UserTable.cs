using System;
using System.Collections.Generic;

namespace DLaura.Entities;

public partial class UserTable
{
    public int TableNumber { get; set; }

    public int MaxCapacity { get; set; }

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
