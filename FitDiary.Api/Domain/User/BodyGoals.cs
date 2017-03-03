using FitDiary.Api.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitDiary.Api.Domain.User
{
    public class BodyGoals
    {
        public double WeightGoal { get; set; }
        public double BodyFatGoal { get; set; }
        public TimeSpan GoalTime { get; set; }

        public bool IsAchieved { get; set; }
        public DateTime? AchievedDate { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}