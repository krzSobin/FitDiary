namespace FitDiary.Contracts.DTOs.Diet
{
    public class ProductInMealDTO
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public double AmountInGrams { get; set; }
        public string Name { get; set; }
        public double Kcal { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbs { get; set; }
        public double Sugar { get; set; }
    }
}
