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

internal sealed class GetCategoryHandler(IEfRepository<Category> _repository)
    : IRequestHandler<GetCategoryQuery, CategoryResponse>
{
    public async Task<CategoryResponse> Handle(GetCategoryQuery query, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(query.categoryId, cancellationToken);

        if (category is null)
        {
            return new CategoryResponse();
        }

        return category.Adapt<CategoryResponse>();
    }
}
