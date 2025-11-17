using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLaura.Entities;

namespace DLaura.BusinessLogic.DTOs
{
    public class CreateLocationrequest
    {
        public int TableNumber { get; set; }

        public string CoordinateX { get; set; } = null!;

        public string CoordinateY { get; set; } = null!;
    }

    public class UpdateLocationRequest
    {
        public int Id { get; set; }

        public int TableNumber { get; set; }

        public string CoordinateX { get; set; } = null!;

        public string CoordinateY { get; set; } = null!;
    }

    public class LocationResponse
    {
        public int TableNumber { get; set; }

        public string CoordinateX { get; set; } = null!;

        public string CoordinateY { get; set; } = null!;

        public virtual UserTable TableNumberNavigation { get; set; } = null!;
    }

    public class TableLocationResponse
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public string CoordinateX { get; set; }
        public string CoordinateY { get; set; }
        public virtual UserTableResponse TableNumberNavigation { get; set; }


    }
}
