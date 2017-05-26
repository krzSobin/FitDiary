using System;

namespace FitDiary.SecuredApi.Models
{
    public class BodyGoals
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
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