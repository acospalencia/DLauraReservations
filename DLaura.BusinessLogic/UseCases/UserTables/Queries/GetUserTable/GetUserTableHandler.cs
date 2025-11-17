using DLaura.BusinessLogic.DTOs;
using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.UserTables.Queries.GetUserTable
{
    internal class GetUserTableHandler(IEfRepository<UserTable> _repository) : IRequestHandler<GetUserTableQuery, List<UserTableResponse>>
    {
        public async Task<List<UserTableResponse>> Handle(GetUserTableQuery query, CancellationToken cancellationToken)
        {
            var Usertable = await _repository.ListAsync(cancellationToken);

            if (Usertable == null || !Usertable.Any())
            {
                return new List<UserTableResponse>();
            }
            return Usertable.Adapt<List<UserTableResponse>>();
        }
    }
}
