using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.UserTables.Commands.DeleteUserTable
{
    internal sealed class DeleteUserTableHandler(IEfRepository<UserTable> _repository) : IRequestHandler<DeleteUserTableCommand, int>
    {
        public async Task<int> Handle(DeleteUserTableCommand command, CancellationToken cancellationToken)
        {
            var existingUserTable = await _repository.GetByIdAsync(command.TableNumber);
            if (existingUserTable is null) return 0;
            await _repository.DeleteAsync(existingUserTable, cancellationToken);
            return existingUserTable.TableNumber;
        }
    }
}
