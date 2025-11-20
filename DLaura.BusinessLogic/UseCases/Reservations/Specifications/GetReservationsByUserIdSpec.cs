using Ardalis.Specification;
using DLaura.Entities;

namespace DLaura.BusinessLogic.UseCases.Reservations.Specifications;

/// <summary>
/// Especificación para obtener todas las reservaciones de un usuario específico.
/// </summary>
public sealed class GetReservationsByUserIdSpec : Specification<Reservation>
{
    public GetReservationsByUserIdSpec(int userId)
    {
        Query
            .Where(r => r.UserId == userId)
            .Include(r => r.User)
            .Include(r => r.TableNumberNavigation)
            .OrderByDescending(r => r.ReservationDate);
    }
}