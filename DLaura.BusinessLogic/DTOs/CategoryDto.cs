using System.Globalization;

namespace DLaura.BusinessLogic.DTOs
{
     public class CreateCategoryRequest
    {
        public string CategoryName { get; set; }
    }
    public class UpdateCategoryRequest
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    public class CategoryResponse
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }  
}
