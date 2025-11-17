using DLaura.BusinessLogic.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLaura.BusinessLogic.UseCases.Reservations.Commands.UpdateReservation
{
    public record UpdateReservationCommand(UpdateReservationRequest Request) : IRequest<int>;
}
