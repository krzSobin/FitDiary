using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitDiary.SecuredApi.Models.User
{
    public class BodyGoals
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public double? WeightInKg { get; set; }
        public double? ChestInCm { get; set; }
        public double? WaistInCm { get; set; }

        public BodyGoalStatus Status { get; set; }
    }

    public enum BodyGoalStatus
    {
        InProgress = 0,
        Achieved = 1,
        NotAchieved = 2,
        Canceled = 3
    }
}