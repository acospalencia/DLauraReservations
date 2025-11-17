using DLaura.BusinessLogic.DTOs;
using DLaura.BusinessLogic.UseCases.Reservations.Specifications;
using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Reservations.Queries.GetReservationByDate
{
    internal sealed class GetReservationByDateHandler(IEfRepository<Reservation> _repository)
   : IRequestHandler<GetReservationByDateQuery, List<ReservationResponse>>
    {
        public async Task<List<ReservationResponse>> Handle(GetReservationByDateQuery query, CancellationToken cancellationToken)
        {
            var reservations = await _repository.ListAsync(new GetReservationByDateSpec(query.request), cancellationToken);
            return reservations.Adapt<List<ReservationResponse>>();
        }
    }
}
