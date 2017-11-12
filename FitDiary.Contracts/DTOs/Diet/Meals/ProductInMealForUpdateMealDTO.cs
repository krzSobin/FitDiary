namespace FitDiary.Contracts.DTOs.Diet.Meals
{
    public class ProductInMealForUpdateMealDTO
    {
        public int? Id { get; set; }
        public double AmountInGrams { get; set; }
        public int ProductId { get; set; }
    }
}
