using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Categorys.Commands.DeleteCategory;

public record DeleteCategoryCommand(int categoryId) :   IRequest<int>;

