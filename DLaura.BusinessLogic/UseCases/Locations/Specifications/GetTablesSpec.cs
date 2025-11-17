using Ardalis.Specification;
using DLaura.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLaura.BusinessLogic.UseCases.Locations.Specifications
{
    public class GetTablesSpec : Specification<Location>
    {
        public GetTablesSpec()
        {
            Query.Include(r => r.TableNumberNavigation);
        }
    }
}
