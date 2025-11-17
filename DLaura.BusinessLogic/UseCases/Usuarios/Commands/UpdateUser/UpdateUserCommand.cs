using DLaura.BusinessLogic.DTOs;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Usuarios.Commands.UpdateUser;

/// <summary>
/// Comando para actualizar la información de un usuario existente.
/// Implementa la interfaz <see cref="IRequest{TResponse}"/> de MediatR.
/// </summary>
/// <param name="Request">Objeto que contiene los datos actualizados del usuario.</param>
public record UpdateUserCommand(UpdateUserRequest Request) : IRequest<int>;
