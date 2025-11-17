using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using MediatR;
using Mapster;

namespace DLaura.BusinessLogic.UseCases.UserTables.Commands.CreateUserTable;

internal sealed class CreateUserTableHandler(IEfRepository<UserTable> _repository) : IRequestHandler<CreateUserTableCommand, int>

{
    public async Task<int> Handle(CreateUserTableCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var newUserTable = command.Request.Adapt<UserTable>();
            var createUserTable = await _repository.AddAsync(newUserTable, cancellationToken);
            return createUserTable.TableNumber;
        }
        catch(Exception)
        {
            return 0;
            throw;
        }
    }
}
