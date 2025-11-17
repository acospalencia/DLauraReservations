using DLaura.BusinessLogic.DTOs;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.UserTables.Commands.CreateUserTable
{
    public record CreateUserTableCommand(CreateUserTableRequest Request) : IRequest<int>;

}
