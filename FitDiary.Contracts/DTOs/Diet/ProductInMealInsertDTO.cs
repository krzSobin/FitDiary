namespace FitDiary.Contracts.DTOs.Diet
{
    public class ProductInMealInsertOrUpdateDTO
    {
        public double AmountInGrams { get; set; }
        public int ProductId { get; set; }
        public int MealId { get; set; }
    }
}
