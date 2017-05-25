namespace FitDiary.Contracts.DTOs.Diet
{
    public class FoodProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double ProteinsPer100g { get; set; }
        public double FatsPer100g { get; set; }
        public double CarboPer100g { get; set; }
        public double SugarPer100g { get; set; }
        public double KCalPer100g { get; set; }
        public string Category { get; set; }
    }
}
