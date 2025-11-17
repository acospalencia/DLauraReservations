using DLaura.BusinessLogic.DTOs;
using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Locations.Queries.GetLocation
{
    internal class GetLocationHandler(IEfRepository<Location> _repository) : IRequestHandler<GetLocationQuery, List<LocationResponse>>
    {
        public async Task<List<LocationResponse>> Handle(GetLocationQuery query, CancellationToken cancellationToken)
        {
            var location = await _repository.ListAsync(cancellationToken);

            if (location == null || !location.Any())
            {
                return new List<LocationResponse>();
            }
            return location.Adapt<List<LocationResponse>>();
        }
    }
}

