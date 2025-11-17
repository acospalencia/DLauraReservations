 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLaura.BusinessLogic.DTOs;
using MediatR;

namespace DLaura.BusinessLogic.UseCases.Categorys.Commands.CreateCategory;

public record CreateCategoryCommand(CreateCategoryRequest Request) : IRequest<int>;


