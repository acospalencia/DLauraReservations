using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLaura.BusinessLogic.UseCases.UserTables.Commands.DeleteUserTable;
using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DLaura.BusinessLogic.UseCases.Locations.Commands.DeleteLocation
{
    internal  sealed class DeleteLocationHandler(IEfRepository<Location> _repository) : IRequestHandler<DeleteLocationCommand, int>
    {
        public async Task<int> Handle(DeleteLocationCommand command, CancellationToken cancellationToken)
        {
            var existingLocation = await _repository.GetByIdAsync(command.Id);
            if (existingLocation is null) return 0;
            await _repository.DeleteAsync(existingLocation, cancellationToken);
            return existingLocation.Id;
        }
    }
}
