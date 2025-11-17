using DLaura.BusinessLogic.DTOs;
using DLaura.BusinessLogic.UseCases.Locations.Specifications;
using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Locations.Queries.GetTableLocation
{
    internal sealed class GetTableLocationHandler(IEfRepository<Location> _repository) : IRequestHandler<GetTableLocationQuerie, List<TableLocationResponse>>
    {
        public async Task<List<TableLocationResponse>> Handle(GetTableLocationQuerie query, CancellationToken cancellationToken)
        {
            var reservation = await _repository.ListAsync(new GetTablesSpec(), cancellationToken);
            if (reservation == null || !reservation.Any())
            {
                return new List<TableLocationResponse>();
            }
            return reservation.Adapt<List<TableLocationResponse>>();
        }
    }

}
