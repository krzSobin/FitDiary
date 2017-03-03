using FitDiary.Api.Domain.User;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitDiary.Api.Auth.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Age { get; set; }
        public int Height { get; set; }

        public double Weight { get; set; }
        public double BodyFat { get; set; }
        public BodyMeasurements Measurements { get; set; }
        public BodyGoals BodyGoals { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }
    }
}