using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitDiary.Contracts.DTOs
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
