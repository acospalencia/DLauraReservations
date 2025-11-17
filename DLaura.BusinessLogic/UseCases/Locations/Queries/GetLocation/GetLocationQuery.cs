using DLaura.BusinessLogic.DTOs;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Locations.Queries.GetLocation
{
    public record GetLocationQuery() : IRequest<List<LocationResponse>>;


}
