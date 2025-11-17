using DLaura.BusinessLogic.DTOs;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.UserTables.Queries.GetUserTable
{
    public record GetUserTableQuery() : IRequest<List<UserTableResponse>>;


}
