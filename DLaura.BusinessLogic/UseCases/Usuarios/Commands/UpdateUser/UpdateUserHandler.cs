using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Usuarios.Commands.UpdateUser;

/// <summary>
/// Manejador para la actualización de usuarios en el sistema.
/// Implementa la interfaz <see cref="IRequestHandler{TRequest, TResponse}"/> de MediatR
/// para procesar el comando <see cref="UpdateUserCommand"/>.
/// </summary>
/// <param name="_repository">Repositorio de acceso a datos para la entidad <see cref="User"/>.</param>
internal sealed class UpdateUserHandler(IEfRepository<User> _repository)
    : IRequestHandler<UpdateUserCommand, int>
{
    /// <summary>
    /// 
    /// 
    /// Maneja la actualización de un usuario en la base de datos.
    /// </summary>
    /// <param name="command">Comando que contiene los datos actualizados del usuario.</param>
    /// <param name="cancellationToken">Token de cancelación para operaciones asíncronas.</param>
    /// <returns>
    /// El identificador del usuario actualizado si la operación es exitosa.
    /// Retorna 0 si el usuario no existe o en caso de error.
    /// </returns>
    public async Task<int> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            // Obtiene el usuario existente desde la base de datos.
            var existingUser = await _repository.GetByIdAsync(command.Request.Id);

            // Si el usuario no existe, retorna 0.
            if (existingUser is null) return 0;

            // Mapea los datos actualizados a la entidad existente.
            existingUser = command.Request.Adapt(existingUser);

            // Actualiza el usuario en la base de datos.
            await _repository.UpdateAsync(existingUser, cancellationToken);

            // Retorna el identificador del usuario actualizado.
            return existingUser.Id;
        }
        catch (Exception)
        {
            // En caso de error, retorna 0.
            return 0;
            throw; // Excepción relanzada para mantener la trazabilidad.
        }
    }
}
