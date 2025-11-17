using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLaura.BusinessLogic.UseCases.Reservations.Commands.DeleteReservation
{
    public record class DeleteReservationCommand(int Id) : IRequest<int>;


}
