using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Promotions.Commads.UpdatePromotion;

internal sealed class UpdatePromotionHandler(IEfRepository<Promotion> _repository)
    : IRequestHandler<UpdatePromotionCommand, int>
{
    public async Task<int> Handle(UpdatePromotionCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var existingPromotion = await _repository.GetByIdAsync(command.Request.PromotionId);

            if (existingPromotion is null) return 0;

            existingPromotion = command.Request.Adapt(existingPromotion);

            await _repository.UpdateAsync(existingPromotion, cancellationToken);

            return existingPromotion.Id;

        }
        catch (Exception)
        {
            return 0;
        }
    }
}