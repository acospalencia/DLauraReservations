using DLaura.BusinessLogic.DTOs;
using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Promotions.Queries.GetPromotion;

internal sealed class GetPromotionHandler(IEfRepository<Promotion> _repository)
    : IRequestHandler<GetPromotionQuery, PromotionResponse>
{
    public async Task<PromotionResponse> Handle(GetPromotionQuery query, CancellationToken cancellationToken)
    {
        var promotion = await _repository.GetByIdAsync(query.promotionId, cancellationToken);
            
        if (promotion is null)
        {
            return new PromotionResponse();
        }
        return promotion.Adapt<PromotionResponse>();
    }
}