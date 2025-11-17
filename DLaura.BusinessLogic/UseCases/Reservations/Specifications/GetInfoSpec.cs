using Ardalis.Specification;
using DLaura.Entities;

namespace DLaura.BusinessLogic.UseCases.Reservations.Specifications
{
    public class GetInfoSpec : Specification<Reservation>
    {
        public GetInfoSpec()
        {
            Query.Include(r => r.TableNumberNavigation);
            Query.Include(r => r.User);
        }
    }
}
