namespace DLaura.BusinessLogic.DTOs
{
    public class CreatePromotionRequest
    {
        public string PromotionName { get; set; }
    }
    public class UpdatePromotionRequest
    {
        public int PromotionId { get; set; }
        public string PromotionName { get; set; }
    }
    public class PromotionResponse
    {
        public int PromotionId { get; set; }
        public string PromotionName { get; set; }
    }
}
