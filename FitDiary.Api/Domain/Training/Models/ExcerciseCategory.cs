using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitDiary.Api.Training.Models
{
    public class ExcerciseCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> MainMuscles { get; set; }
        public List<string> HelpersMuscles { get; set; }
    }
}