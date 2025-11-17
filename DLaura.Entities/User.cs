using System;
using System.Collections.Generic;

namespace DLaura.Entities;

public partial class User
{
    public int Id { get; set; }

    public int RolId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public string UserPassword { get; set; } = null!;

    public bool? Compensation { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual Role Rol { get; set; } = null!;
}
