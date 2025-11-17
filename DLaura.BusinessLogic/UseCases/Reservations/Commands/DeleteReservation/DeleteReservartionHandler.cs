using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLaura.BusinessLogic.UseCases.Reservations.Commands.DeleteReservation
{
    internal sealed class DeleteReservartionHandler(IEfRepository<Reservation> _repository) : IRequestHandler<DeleteReservationCommand, int>
    {

        public async Task<int> Handle(DeleteReservationCommand command, CancellationToken cancellationToken)
        {

            var findReservation = await _repository.GetByIdAsync(command.Id);

            if (findReservation != null)
            {
                return 0;
            }
            await _repository.DeleteAsync(findReservation, cancellationToken);
            return findReservation.Id;



        }
    }
}

