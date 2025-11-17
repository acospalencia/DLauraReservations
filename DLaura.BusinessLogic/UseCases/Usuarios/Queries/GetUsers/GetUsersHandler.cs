using DLaura.BusinessLogic.DTOs;
using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Usuarios.Queries.GetUsers
{
    internal sealed class GetUsersHandler : IRequestHandler<GetUsersQuery, List<UserResponse>>
    {
        private readonly IEfRepository<User> _repository;

        public GetUsersHandler(IEfRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<List<UserResponse>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var users = await _repository.ListAsync(cancellationToken);

            if (users == null || !users.Any())
            {
                return new List<UserResponse>();
            }

            return users.Adapt<List<UserResponse>>();
        }
    }
}
