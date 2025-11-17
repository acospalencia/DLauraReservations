using DLaura.BusinessLogic.DTOs;
using DLaura.BusinessLogic.UseCases.Reservations.Specifications;
using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Reservations.Queries.GetReservation
{
    internal sealed class GetReservationsHandler(IEfRepository<Reservation> _repository) : IRequestHandler<GetReservationsQuerie, List<ReservationResponse>>
    {
        public async Task<List<ReservationResponse>> Handle(GetReservationsQuerie query, CancellationToken cancellationToken)
        {
            var reservation = await _repository.ListAsync(new GetInfoSpec(), cancellationToken);

            if (reservation == null || !reservation.Any())
            {
                return new List<ReservationResponse>();
            }
            return reservation.Adapt<List<ReservationResponse>>();
        }
    }

}
