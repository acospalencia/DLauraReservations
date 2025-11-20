using DLaura.BusinessLogic.DTOs;
using DLaura.BusinessLogic.UseCases.Reservations.Specifications;
using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Reservations.Queries.GetReservation;

/// <summary>
/// Handler para obtener las reservaciones de un usuario específico.
/// </summary>
internal sealed class GetReservationHandler : IRequestHandler<GetReservationQuerie, List<ReservationResponse>>
{
    private readonly IEfRepository<Reservation> _repository;

    public GetReservationHandler(IEfRepository<Reservation> repository)
    {
        _repository = repository;
    }

    public async Task<List<ReservationResponse>> Handle(GetReservationQuerie request, CancellationToken cancellationToken)
    {
        try
        {
            // Usa la especificación para filtrar por UserId
            var reservations = await _repository.ListAsync(
                new GetReservationsByUserIdSpec(request.request.Id),
                cancellationToken
            );

            // Si no hay reservaciones, retorna lista vacía
            if (reservations == null || !reservations.Any())
            {
                return new List<ReservationResponse>();
            }

            // Mapea las reservaciones a ReservationResponse
            return reservations.Adapt<List<ReservationResponse>>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en GetReservationHandler: {ex.Message}");
            return new List<ReservationResponse>();
        }
    }
}