using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLaura.DataAcces.Interfaces;
using DLaura.DataAcces.Repositories;
using DLaura.Entities;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Promotions.Commads.DeletePromotion;

internal sealed class DeletePromotionHandler(IEfRepository<Promotion> _repository)
    : IRequestHandler<DeletePromotionCommand, int>
{
    public async Task<int> Handle(DeletePromotionCommand command, CancellationToken cancellationToken)
    {
        var existingPromotion = await _repository.GetByIdAsync(command.categoryId);

        if (existingPromotion is null) return 0;

        await _repository.DeleteAsync(existingPromotion, cancellationToken);

        return existingPromotion.Id;
    }
}

