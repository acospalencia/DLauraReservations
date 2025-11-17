using DLaura.BusinessLogic.DTOs;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Usuarios.Commands.CreateUser
{
    public record CreateUserCommand(CreateUserRequest Request) : IRequest<int>;
}
