using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitDiary.Contracts.DTOs.Diet
{
    public class DietDayDTO
    {
        public DateTime Date { get; set; }
        public int MealsCount { get; set; }
        public double TotalKCal { get; set; }
        public double RealizationPercent { get; set; }
        public ICollection<double> Macros { get; set; }
    }
}
