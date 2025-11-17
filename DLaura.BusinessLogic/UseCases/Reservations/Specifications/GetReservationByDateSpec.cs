using Ardalis.Specification;
using DLaura.BusinessLogic.DTOs;
using DLaura.Entities;

namespace DLaura.BusinessLogic.UseCases.Reservations.Specifications
{
    public class GetReservationByDateSpec : Specification<Reservation>
    {
        public GetReservationByDateSpec(DateFilterRequest request)
        {
            Query.Include(r => r.TableNumberNavigation);
            Query.Include(r => r.User);
            Query.Where(r => r.ReservationDate == request.ReservationDate);
        }
    }
}
