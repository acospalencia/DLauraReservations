using DLaura.BusinessLogic.DTOs;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Locations.Commands.UpdateLocation
{
    public record UpdateLocationCommand(UpdateLocationRequest Request) : IRequest<int>
    {
    }
}
