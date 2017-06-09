﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitDiary.SecuredApi.Models.Training
{
    public class Excercise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }


        public virtual ICollection<MuscleInExcercise> Muscles { get; set; }
        public virtual ICollection<ExcerciseSerie> Series { get; set; }
    }

    public enum Muscles
    {
        Chest,
        Back,
        Triceps,
        Biceps
    }
}