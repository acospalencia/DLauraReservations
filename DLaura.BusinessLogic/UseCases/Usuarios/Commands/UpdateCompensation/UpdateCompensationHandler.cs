using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Usuarios.Commands.UpdateCompensation;

/// <summary>
/// Manejador para actualizar la compensación de un usuario.
/// </summary>
internal sealed class UpdateCompensationHandler(IEfRepository<User> _repository)
    : IRequestHandler<UpdateCompensationCommand, bool>
{
    /// <summary>
    /// Actualiza el campo de compensación de un usuario específico.
    /// </summary>
    /// <param name="command">Comando con el ID del usuario y valor de compensación.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>True si la actualización fue exitosa, false en caso contrario.</returns>
    public async Task<bool> Handle(UpdateCompensationCommand command, CancellationToken cancellationToken)
    {
        try
        {
            // Obtiene el usuario existente desde la base de datos
            var existingUser = await _repository.GetByIdAsync(command.UserId);

            // Si el usuario no existe, retorna false
            if (existingUser is null) return false;

            // Actualiza solo el campo de compensación
            existingUser.Compensation = command.Compensation;

            // Guarda los cambios en la base de datos
            await _repository.UpdateAsync(existingUser, cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}