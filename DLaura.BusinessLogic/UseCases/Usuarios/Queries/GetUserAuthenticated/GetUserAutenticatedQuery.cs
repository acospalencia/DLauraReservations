using DLaura.BusinessLogic.DTOs;
using MediatR;


namespace DLaura.BusinessLogic.UseCases.Usuarios.Queries.GetUserAuthenticated
{
    /// <summary>
    /// Consulta para obtener la información de un usuario autenticado.
    /// Implementa la interfaz <see cref="IRequest{TResponse}"/> de MediatR para manejar la solicitud de autenticación de un usuario.
    /// </summary>
    public record GetUserAuthenticatedQuery(string Email, string password) : IRequest<UserResponse>;
}

