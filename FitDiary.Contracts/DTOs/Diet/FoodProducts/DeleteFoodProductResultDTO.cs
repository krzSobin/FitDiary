namespace FitDiary.Contracts.DTOs.Diet.FoodProducts
{
    public class DeleteFoodProductResultDTO
    {
        public bool Deleted { get; set; }
        public FoodProductDTO FoodProduct { get; set; }
    }
}
