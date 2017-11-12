namespace FitDiary.Contracts.DTOs.Diet.FoodProducts
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double ProteinsPer100g { get; set; }
        public double FatsPer100g { get; set; }
        public double CarboPer100g { get; set; }
        public double SugarPer100g { get; set; }
        public double KCalPer100g { get; set; }
        public int CategoryId { get; set; }
    }
}
