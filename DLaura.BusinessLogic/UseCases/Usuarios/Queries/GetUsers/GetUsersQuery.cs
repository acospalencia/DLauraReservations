using DLaura.BusinessLogic.DTOs;
using DLaura.Entities; // Importa las entidades, en este caso la entidad User.
using MediatR; // Importa MediatR, una librería para manejar el patrón Mediator en C#.

namespace DLaura.BusinessLogic.UseCases.Usuarios.Queries.GetUsers
{
    /// <summary>
    /// Consulta para obtener la lista de todos los usuarios.
    /// </summary>
    /// <remarks>
    /// Esta clase representa una consulta que se utiliza para obtener todos los usuarios del sistema.
    /// El patrón Mediator es utilizado para manejar esta consulta de manera desacoplada, y se espera 
    /// que el resultado sea una lista de objetos <see cref="User"/>.
    /// </remarks>
    public record GetUsersQuery() : IRequest<List<UserResponse>>;
    // La consulta no requiere parámetros y se espera que se devuelva una lista de usuarios.
}
