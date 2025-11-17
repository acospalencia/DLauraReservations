using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Categorys.Commands.DeleteCategory;

internal sealed class DeleteCategoryHandler(IEfRepository<Category> _repository)
    : IRequestHandler<DeleteCategoryCommand, int>
{
    public async Task<int> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var existingCategory = await _repository.GetByIdAsync(command.categoryId);

        if (existingCategory is null) return 0;

        await _repository.DeleteAsync(existingCategory, cancellationToken);

        return existingCategory.Id;
    }
}
   
