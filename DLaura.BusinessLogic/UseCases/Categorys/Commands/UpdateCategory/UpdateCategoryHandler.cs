using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Categorys.Commands.UpdateCategory;

internal sealed class UpdateCategoryHandler(IEfRepository<Category> _repository)
        : IRequestHandler<UpdateCategoryCommand, int>
{
    public async Task<int> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var existingCategory = await _repository.GetByIdAsync(command.Request.CategoryId);

            if (existingCategory is null) return 0;

            existingCategory = command.Request.Adapt(existingCategory);

            await _repository.UpdateAsync(existingCategory, cancellationToken);

            return existingCategory.Id;
        }
        catch (Exception)
        {
            return 0;
            throw;
        }
    }
}
