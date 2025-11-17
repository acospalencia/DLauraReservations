using DLaura.BusinessLogic.DTOs;
using DLaura.BusinessLogic.UseCases.Usuarios.Commands.Queries.GetRoles;
using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Usuarios.Queries.GetRoles;

/// <summary>
/// Manejador para procesar la consulta de obtener roles en el sistema.
/// Implementa <see cref="IRequestHandler{TRequest, TResponse}"/> de MediatR para manejar la consulta <see cref="GetRolesQuery"/>.
/// </summary>
/// <param name="_repository">Repositorio para acceder a los datos de la entidad <see cref="Role"/>.</param>
internal sealed class GetRolesHandler(IEfRepository<Role> _repository)
    : IRequestHandler<GetRolesQuery, List<RoleResponse>>
{
    /// <summary>
    /// Maneja la consulta para obtener la lista de roles disponibles.
    /// </summary>
    /// <param name="query">Consulta que solicita la lista de roles.</param>
    /// <param name="cancellationToken">Token de cancelación para operaciones asíncronas.</param>
    /// <returns>
    /// Lista de objetos <see cref="RoleResponse"/> que representan los roles disponibles.
    /// Si no hay roles, retorna una lista vacía.
    /// </returns>
    public async Task<List<RoleResponse>> Handle(GetRolesQuery query, CancellationToken cancellationToken)
    {
        var roles = await _repository.ListAsync(cancellationToken);

        if (roles == null || !roles.Any())
        {
            return new List<RoleResponse>();
        }

        // Mapeo directo (ahora las propiedades coinciden)
        return roles.Adapt<List<RoleResponse>>();
    }
}
