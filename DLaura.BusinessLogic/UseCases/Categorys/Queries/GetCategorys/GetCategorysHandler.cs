using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLaura.BusinessLogic.DTOs;
using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Categorys.Queries.GetCategory;

internal sealed class GetCategorysHandler(IEfRepository<Category> _repository)
    : IRequestHandler<GetCategorysQuery, List<CategoryResponse>>
{
    public async Task<List<CategoryResponse>> Handle(GetCategorysQuery query, CancellationToken cancellationToken)
    {
        var categorys = await _repository.ListAsync(cancellationToken);

        if (categorys == null || !categorys.Any())
        {
            return new List<CategoryResponse>();
        }

        return categorys.Adapt<List<CategoryResponse>>();
    }
}