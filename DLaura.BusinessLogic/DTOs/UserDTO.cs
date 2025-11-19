using DLaura.Entities;

namespace DLaura.BusinessLogic.DTOs;

public class CreateUserRequest
{
    public int RolId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; }

    public string UserPassword { get; set; } = null!;
    public bool Compensation { get; set; } = false; // Por defecto será false
}
public class UpdateUserRequest
{
    public int Id { get; set; }
    public int RolId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string? Email { get; set; }
    public bool Compensation { get; set; } = false; 
}
public class UserResponse
{
    public int Id { get; set; }
    public int RolId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string? Email { get; set; }
    public bool? Compensation { get; set; }
    public string RoleName { get; set; } = null!;
}
public class RoleResponse
{
    public int Id { get; set; }  
    public string RoleName { get; set; } = null!;
}

public class UserByIdResponse 
{
    public int Id { get; set; }

    public int RolId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public string UserPassword { get; set; } = null!;

    public bool? Compensation { get; set; }


}
