using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.UserTables.Commands.UpdateUserTable
{
    internal sealed class UpdateUserTableHandler(IEfRepository<UserTable> _repository) : IRequestHandler<UpdateUserTableCommand, int>
    {
        public async Task<int> Handle(UpdateUserTableCommand command, CancellationToken cancellationToken)
        {
           try
            {
                var existingUserTable = await _repository.GetByIdAsync(command.Request.TableNumber);
                if (existingUserTable is null)
                existingUserTable = command.Request.Adapt(existingUserTable);
                await _repository.UpdateAsync(existingUserTable, cancellationToken);
                return existingUserTable.TableNumber;
            }
            catch(Exception)
            {
                return 0;
                throw;
            }
        }
    }
}
