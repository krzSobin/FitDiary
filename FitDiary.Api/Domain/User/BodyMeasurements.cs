using FitDiary.Api.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitDiary.Api.Domain.User
{
    public class BodyMeasurements
    {
        public int Id { get; set; }
        public double ArmInCm { get; set; }
        public double ForeArmInCm { get; set; }
        public double ChestInCm { get; set; }
        public double WaistInCm { get; set; }
        public double CalvesInCm { get; set; }
        public double LegInCm { get; set; }

        public ApplicationUser User { get; set; }
    }
}