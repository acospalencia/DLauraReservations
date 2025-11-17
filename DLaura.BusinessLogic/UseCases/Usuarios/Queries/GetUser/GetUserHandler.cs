using DLaura.BusinessLogic.DTOs;
using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLaura.BusinessLogic.UseCases.Usuarios.Queries.GetUser;

internal sealed class GetUserHandler(IEfRepository<User> _repository) : IRequestHandler<GetUserQuery, UserByIdResponse>
{
    public async Task<UserByIdResponse> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(query.UserId, cancellationToken);
        if (user == null)
        {
        return new UserByIdResponse();
        }

        return user.Adapt<UserByIdResponse>();
    }
}
