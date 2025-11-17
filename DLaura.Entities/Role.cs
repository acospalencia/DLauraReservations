using System;
using System.Collections.Generic;

namespace DLaura.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
