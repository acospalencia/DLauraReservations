using MediatR;

namespace DLaura.BusinessLogic.UseCases.Locations.Commands.DeleteLocation
{
    public record DeleteLocationCommand(int Id) : IRequest<int>;

}
