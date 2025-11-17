using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Promotions.Commads.CreatePromotion;
internal sealed class CreatePromotionHandler(IEfRepository<Promotion> _repository)
    : IRequestHandler<CreatePromotionCommand, int>
{
    public async Task<int> Handle(CreatePromotionCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var newPromotion = command.Request.Adapt<Promotion>();

            var createdPromotion = await _repository.AddAsync(newPromotion, cancellationToken);

            return createdPromotion.Id;
        }
        catch (Exception)
        {
            return 0;

            throw;
        }
    }
}

