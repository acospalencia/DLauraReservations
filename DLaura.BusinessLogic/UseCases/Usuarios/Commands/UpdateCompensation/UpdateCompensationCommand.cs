using MediatR;

namespace DLaura.BusinessLogic.UseCases.Usuarios.Commands.UpdateCompensation;

/// <summary>
/// Comando para actualizar la compensación de un usuario.
/// </summary>
/// <param name="UserId">ID del usuario a compensar.</param>
/// <param name="Compensation">Valor de compensación (true/false).</param>
public record UpdateCompensationCommand(int UserId, bool Compensation) : IRequest<bool>;