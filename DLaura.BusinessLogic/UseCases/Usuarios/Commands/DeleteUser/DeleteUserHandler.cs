using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLaura.BusinessLogic.UseCases.Usuarios.Commands.DeleteUser
{
    internal sealed class DeleteUserHandler(IEfRepository<User> _repository) : IRequestHandler<DeleteUserCommand, int>
    {
        public async Task<int> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await _repository.GetByIdAsync(command.IdUsuario);
            if (existingUser is null) return 0;
            await _repository.DeleteAsync(existingUser, cancellationToken);
            return existingUser.Id;
        }
    }
}
