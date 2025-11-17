using MediatR;

namespace DLaura.BusinessLogic.UseCases.UserTables.Commands.DeleteUserTable
{
    public record DeleteUserTableCommand(int TableNumber) : IRequest<int>;


}
