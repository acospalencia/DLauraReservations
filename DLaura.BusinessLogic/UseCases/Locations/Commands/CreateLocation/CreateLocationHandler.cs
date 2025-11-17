using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Locations.Commands.CreateLocation
{
    internal sealed class CreateLocationHandler(IEfRepository<Location> _repository) : IRequestHandler<CreateLocationCommand, int>
    {
        public async Task<int> Handle(CreateLocationCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var newLocation = command.Request.Adapt<Location>();
                var CreatenewLocation = await _repository.AddAsync(newLocation, cancellationToken);
                return CreatenewLocation.Id;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }
    }
}
