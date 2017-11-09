using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitDiary.SecuredApi.User.Models
{
    public class UserData
    {
        [Key]
        [DatabaseGe‌​nerated(DatabaseGen‌​eratedOption.None)]
        public int UserDataId { get; set; }
        public DateTime? Birthday { get; set; }
        public string Hobby { get; set; }
        public string City { get; set; }
        public int? HeightInCm { get; set; }
    }
}