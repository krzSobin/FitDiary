using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitDiary.Contracts.DTOs.User
{
    public class BodyMeasurementsDTO
    {
        public double? WeightInKg { get; set; }
        public double? BodyFat { get; set; }
        public double? ChestInCm { get; set; }
        public double? WaistInCm { get; set; }

        public DateTime MeasurementDate { get; set; }
    }
}
