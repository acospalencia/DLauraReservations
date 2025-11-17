using DLaura.BusinessLogic.DTOs;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Usuarios.Commands.Queries.GetRoles;

/// <summary>
/// Consulta para obtener la lista de roles disponibles en el sistema.
/// Implementa la interfaz <see cref="IRequest{TResponse}"/> de MediatR.
/// </summary>
public record GetRolesQuery() : IRequest<List<RoleResponse>>;

    