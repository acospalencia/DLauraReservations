using Ardalis.Specification;
using DLaura.BusinessLogic.DTOs;
using DLaura.Entities;

namespace DLaura.BusinessLogic.UseCases.Reservations.Specifications
{
    public class GetReservationsByDateAndShiftSpec : Specification<Reservation>
    {
        public GetReservationsByDateAndShiftSpec(ReservationByFilterRequest request)
        {
            Query.Where(r => r.ReservationDate == request.ReservationDate && r.ReservateShift == request.ReservateShift);
            Query.Include(r => r.TableNumberNavigation);
            Query.Include(r => r.User);

        }

    }
}
