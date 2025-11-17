using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLaura.BusinessLogic.DTOs;
using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.UserTables.Queries.GetUserTables;

internal sealed class GetUserTableHandler(IEfRepository<UserTable> _repository) : IRequestHandler<GetUserTableQuery, UserTableResponse>
{
    public  async Task<UserTableResponse> Handle(GetUserTableQuery query, CancellationToken cancellationToken)
    {
        var Usertable = await _repository.GetByIdAsync(query.TableNumber, cancellationToken);

        if (Usertable is null) 
        {
            return new UserTableResponse();
        }
        return Usertable.Adapt<UserTableResponse>();
    }
}
