using DLaura.BusinessLogic.DTOs; // Importa los DTOs relacionados con los usuarios.
using DLaura.BusinessLogic.UseCases.Usuarios.Specifications; // Importa las especificaciones necesarias para realizar la consulta.
using DLaura.DataAcces.Interfaces; // Importa las interfaces de acceso a datos (repositorios).
using DLaura.Entities; // Importa las entidades, en este caso la entidad User.
using Mapster; // Importa la librería Mapster para realizar el mapeo de objetos.
using MediatR; // Importa MediatR, una librería para manejar el patrón Mediator en C#.

namespace DLaura.BusinessLogic.UseCases.Usuarios.Queries.GetUserAuthenticated
{
  
   internal sealed class GetUserAutenticatedHandler : IRequestHandler<GetUserAuthenticatedQuery, UserResponse>
    {
        private readonly IEfRepository<User> _repository; // Repositorio para interactuar con la base de datos.
        public GetUserAutenticatedHandler(IEfRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<UserResponse> Handle(GetUserAuthenticatedQuery query, CancellationToken cancellationToken)
        {
            // Realiza la consulta al repositorio usando la especificación definida.
            var user = await _repository.FirstOrDefaultAsync(
                new GetUserAuthenticatedSpec(query.Email, query.password),
                cancellationToken
            );

            // Si no se encuentra el usuario, se retorna un objeto vacío.
            if (user == null)
            {
                return new UserResponse();
            }
            var userResponse = user.Adapt<UserResponse>();


            // Mapear el RoleName desde el objeto User a UserResponse
            userResponse.RoleName = user.Rol.RoleName;

            return userResponse;
        }
    }
}
