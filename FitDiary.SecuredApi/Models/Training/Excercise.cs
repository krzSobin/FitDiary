using System.Collections.Generic;
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
        public double TimeInMinutes { get; set; }

        public IEnumerable<Muscles> MainMuscles { get; set; }
        public IEnumerable<Muscles> HelpersMuscles { get; set; }


        public virtual IEnumerable<ExcerciseSerie> Series { get; set; }
    }

    public enum Muscles
    {
        Chest,
        Back,
        Triceps,
        Biceps
    }
}