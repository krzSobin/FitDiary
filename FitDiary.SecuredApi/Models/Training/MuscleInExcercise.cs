using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitDiary.SecuredApi.Models.Training
{
    public class MuscleInExcercise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsMainMuscle { get; set; }


        public int MuscleId { get; set; }
        public int ExcerciseId { get; set; }

        public virtual Muscle Muscle { get; set; }
        public virtual Excercise Excercise { get; set; }
    }
}