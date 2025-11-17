using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLaura.BusinessLogic.UseCases.Reservations.Commands.UpdateReservation
{
    internal sealed class UpdateReservationHandler(IEfRepository<Reservation> _repository) : IRequestHandler<UpdateReservationCommand, int>
    {
        public async Task<int> Handle(UpdateReservationCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var findReservation = await _repository.GetByIdAsync(command.Request.Id);
                
                if (findReservation != null)
                {
                    return 0;
                }

                findReservation = command.Request.Adapt(findReservation);

                await _repository.UpdateAsync(findReservation, cancellationToken);
                return findReservation.Id;
            }
            catch (Exception)
            {
                return 0;
                throw;

            }
        }
    }
}
