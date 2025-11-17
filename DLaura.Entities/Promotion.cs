using System;
using System.Collections.Generic;

namespace DLaura.Entities;

public partial class Promotion
{
    public int Id { get; set; }

    public int RolId { get; set; }

    public int CategoryId { get; set; }

    public string? Img { get; set; }

    public string Title { get; set; } = null!;

    public string Comment { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Role Rol { get; set; } = null!;
}
