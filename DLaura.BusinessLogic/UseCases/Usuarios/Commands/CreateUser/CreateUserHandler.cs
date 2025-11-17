using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Usuarios.Commands.CreateUser;

/// <summary>
/// Manejador para la creación de usuarios en el sistema.
/// Implementa la interfaz <see cref="IRequestHandler{TRequest, TResponse}"/> de MediatR
/// para procesar el comando <see cref="CreateUserCommand"/>.
/// </summary>
/// <param name="_repository">Repositorio de acceso a datos para la entidad <see cref="User"/>.</param>
internal sealed class CreateUserHandler(IEfRepository<User> _repository)
    : IRequestHandler<CreateUserCommand, int>
{
    /// <summary>
    /// Maneja la creación de un nuevo usuario en la base de datos.
    /// </summary>
    /// <param name="command">Comando que contiene los datos del usuario a crear.</param>
    /// <param name="cancellationToken">Token de cancelación para operaciones asíncronas.</param>
    /// <returns>
    /// El identificador del usuario creado si la operación es exitosa.
    /// Retorna 0 en caso de error.
    /// </returns>
    public async Task<int> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            // Convierte el objeto de la solicitud a la entidad User utilizando Mapster.
            var newUser = command.Request.Adapt<User>();

            // Agrega el nuevo usuario a la base de datos de manera asíncrona.
            var createUser = await _repository.AddAsync(newUser, cancellationToken);

            // Retorna el identificador del usuario creado.
            return createUser.Id;
        }
        catch (Exception)
        {
            // En caso de error, retorna 0.
            return 0;
            throw; // Excepción relanzada para mantener la trazabilidad.
        }
    }
}
