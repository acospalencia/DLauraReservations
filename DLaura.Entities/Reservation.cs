using System;
using System.Collections.Generic;

namespace DLaura.Entities;

public partial class Reservation
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int TableNumber { get; set; }

    public DateOnly ReservationDate { get; set; }

    public string ReservateShift { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public virtual UserTable TableNumberNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
