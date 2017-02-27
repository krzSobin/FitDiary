using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitDiary.Api.Models
{
    public class Excercise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ExcerciseSerie> Series { get; set; }
        public double TimeInMinutes { get; set; }

        public int CategoryId { get; set; }
        public ExcerciseCategory Category { get; set; }
    }
}