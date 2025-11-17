using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLaura.BusinessLogic.DTOs
{
    public class CreateUserTableRequest
    {
        public int TableNumber { get; set; }
        public int MaxCapacity { get; set; }

    }

    public class UpdateUserTableRequest
    { 
        public int TableNumber { get; set; }
        public int MaxCapacity { get; set; }
    }

    public class UserTableResponse
    {
        public int TableNumber { get; set; }
        public int MaxCapacity { get; set; }

    }
}

