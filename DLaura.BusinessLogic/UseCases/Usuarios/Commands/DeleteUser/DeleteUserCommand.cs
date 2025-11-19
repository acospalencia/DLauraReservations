using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLaura.BusinessLogic.UseCases.Usuarios.Commands.DeleteUser
{
    public record DeleteUserCommand(int IdUsuario) : IRequest<int>;
}
