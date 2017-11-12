namespace FitDiary.Contracts.DTOs.Diet.Meals
{
    public class DeleteMealResultDTO
    {
        public bool Deleted { get; set; }
        public MealForListingDTO Meal { get; set; }
    }
}
