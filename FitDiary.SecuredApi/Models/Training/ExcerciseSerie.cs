﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitDiary.SecuredApi.Models.Training
{
    public class ExcerciseSerie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double Weight { get; set; }
        public int Reps { get; set; }
        public double TimeInMinutes { get; set; }


        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }

        public int ExcerciseId { get; set; }
        public Excercise Excercise { get; set; }
    }
}