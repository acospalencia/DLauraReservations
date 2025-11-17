using DLaura.BusinessLogic.DTOs;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Promotions.Queries.GetPromotion;

public record GetPromotionQuery(int promotionId) : IRequest<PromotionResponse>;
