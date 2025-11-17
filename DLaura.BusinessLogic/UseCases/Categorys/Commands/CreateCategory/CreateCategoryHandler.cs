using DLaura.DataAcces.Interfaces;
using DLaura.Entities;
using Mapster;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Categorys.Commands.CreateCategory;

internal sealed class CreateCategoryHandler(IEfRepository<Category> _repository)
    : IRequestHandler<CreateCategoryCommand, int>
{
    public async Task<int> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var newCategory = command.Request.Adapt<Category>();

            var createdCategory = await _repository.AddAsync(newCategory, cancellationToken);

            return createdCategory.Id;
        }
        catch (Exception)
        {
            return 0;
            throw;
        }
    }
}

