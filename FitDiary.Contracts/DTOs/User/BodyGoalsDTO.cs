using System;

namespace FitDiary.Contracts.DTOs.User
{
    public class BodyGoalsDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public double? WeightInKg { get; set; }
        public double? ChestInCm { get; set; }
        public double? WaistInCm { get; set; }

        public int DaysLeftToEnd
        {
            get
            {
                return (EndDate - DateTime.Now).Days;
            }
        }

        private BodyGoalStatus status;
        public string Status
        {
            get
            {
                return status.ToString();
            }
            set
            {
                if (!Enum.TryParse(value, out status))
                {
                    status = BodyGoalStatus.None;
                }
                
            }
        }
    }

    public enum BodyGoalStatus
    {
        None = 0,
        InProgress = 1,
        Achieved = 2,
        NotAchieved = 3,
        Canceled = 4
    }
}
