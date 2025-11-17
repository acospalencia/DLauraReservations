using DLaura.BusinessLogic.DTOs;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Locations.Commands.CreateLocation
{
    public record CreateLocationCommand(CreateLocationrequest Request) : IRequest<int>;
    
    
}
