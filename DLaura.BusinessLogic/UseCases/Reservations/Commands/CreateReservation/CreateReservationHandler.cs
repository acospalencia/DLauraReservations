using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using MediatR;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLaura.BusinessLogic.UseCases.Reservations.Commands.CreateReservation
{
    internal sealed class CreateReservationHandler(IEfRepository<Reservation> _repository) : IRequestHandler<CreateReservationCommand, int>
    {
        public async Task<int> Handle(CreateReservationCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var newReservation = command.Request.Adapt<Reservation>();

                var createdReservation = await _repository.AddAsync(newReservation, cancellationToken);

                return createdReservation.Id;
            }
            catch(Exception x)
            {
                return 0;
                throw;
            }
        }
    }
}
