using DLaura.BusinessLogic.DTOs;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.UserTables.Commands.UpdateUserTable
{
    public record UpdateUserTableCommand(UpdateUserTableRequest Request) : IRequest<int>
    {

    }
}
