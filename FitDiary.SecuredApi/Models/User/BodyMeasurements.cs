using FitDiary.SecuredApi.User.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitDiary.SecuredApi.Models.User
{
    public class BodyMeasurements
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public double? WeightInKg { get; set; }
        public double? BodyFat { get; set; }
        public double? ChestInCm { get; set; }
        public double? WaistInCm { get; set; }

        public DateTime MeasurementDate { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}