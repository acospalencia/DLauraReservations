using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLaura.BusinessLogic.DTOs;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Categorys.Commands.UpdateCategory;

public record UpdateCategoryCommand(UpdateCategoryRequest Request) :  IRequest<int>;

