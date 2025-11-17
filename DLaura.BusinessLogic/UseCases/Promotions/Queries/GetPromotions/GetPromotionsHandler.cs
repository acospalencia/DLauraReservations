using DLaura.BusinessLogic.DTOs;
using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Promotions.Queries.GetPromotion;

internal sealed class GetPromotionsHandler(IEfRepository<Promotion> _repository)
  : IRequestHandler<GetPromotionsQuery, List<PromotionResponse>>
{
    public async Task<List<PromotionResponse>> Handle(GetPromotionsQuery query, CancellationToken cancellationToken)
    {
        var promotions = await _repository.ListAsync(cancellationToken);

        if (promotions == null || !promotions.Any())
        {
            return new List<PromotionResponse>();
        }
        return promotions.Adapt<List<PromotionResponse>>();

    }
}
