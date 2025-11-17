using DLaura.BusinessLogic.DTOs;
using DLaura.BusinessLogic.UseCases.Reservations.Specifications;
using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Reservations.Queries.GetReservationsByDateAndShift
{
    internal sealed class GetReservationsByDateAndShiftHandler(IEfRepository<Reservation> _repository)
        : IRequestHandler<GetReservationsByDateAndShiftQuery, List<ReservationResponse>>
    {
        public async Task<List<ReservationResponse>> Handle(GetReservationsByDateAndShiftQuery query, CancellationToken cancellationToken)
        {
            var reservations = await _repository.ListAsync(new GetReservationsByDateAndShiftSpec (query.request), cancellationToken);

            
            return reservations.Adapt<List<ReservationResponse>>();
        }
    }
}
