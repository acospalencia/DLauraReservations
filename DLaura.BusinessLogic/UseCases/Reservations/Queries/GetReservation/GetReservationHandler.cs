using DLaura.BusinessLogic.DTOs;
using DLaura.BusinessLogic.UseCases.Reservations.Specifications;
using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Reservations.Queries.GetReservation
{
    internal sealed class GetReservationHandler(IEfRepository<Reservation> _repository) : IRequestHandler<GetReservationQuerie, List<ReservationResponse>>
    {
        public async Task<List<ReservationResponse>> Handle(GetReservationQuerie querie, CancellationToken cancellationToken)
        {
            var findReservation = await _repository.ListAsync(new GetInfoSpec(), cancellationToken);

            if (findReservation is null)
            {
                return new List<ReservationResponse>();
            }
            return findReservation.Adapt<List<ReservationResponse>>();

        }
    }
}
    