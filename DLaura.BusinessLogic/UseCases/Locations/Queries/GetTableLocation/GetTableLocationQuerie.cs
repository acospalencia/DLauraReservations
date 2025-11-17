using DLaura.BusinessLogic.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLaura.BusinessLogic.UseCases.Locations.Queries.GetTableLocation
{
    public record GetTableLocationQuerie() : IRequest<List<TableLocationResponse>>;
 
}
