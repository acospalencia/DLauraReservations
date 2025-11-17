using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Promotions.Commads.DeletePromotion;

public record DeletePromotionCommand(int categoryId) : IRequest<int>;