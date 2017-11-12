namespace FitDiary.Contracts.DTOs.Diet.FoodProducts
{
    public class ProductInsertDTO
    {
        public int Id { get; set; } //TODO chyba mozna usunąć
        public string Name { get; set; }
        public double ProteinsPer100g { get; set; }
        public double FatsPer100g { get; set; }
        public double CarboPer100g { get; set; }
        public double SugarPer100g { get; set; }
        public double KCalPer100g { get; set; }
        public int CategoryId { get; set; }
    }
}
