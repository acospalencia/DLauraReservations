using DLaura.BusinessLogic.UseCases.UserTables.Commands.UpdateUserTable;
using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Locations.Commands.UpdateLocation
{
    internal sealed class UpdateLocationHandler(IEfRepository<Location> _repository) : IRequestHandler<UpdateLocationCommand, int>
    {
        public async Task<int> Handle(UpdateLocationCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existingLocation = await _repository.GetByIdAsync(command.Request.Id);

                if (existingLocation is null)
                {
                    return 0; 
                }

                existingLocation.CoordinateX = command.Request.CoordinateX;
                existingLocation.CoordinateY = command.Request.CoordinateY;
                existingLocation.TableNumber = command.Request.TableNumber;

                await _repository.UpdateAsync(existingLocation, cancellationToken);
                return existingLocation.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando la ubicación: {ex.Message}");
                return 0;
            }
        }
    }
}
