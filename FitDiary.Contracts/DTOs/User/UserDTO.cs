using System;

namespace FitDiary.Contracts.DTOs.User
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public int? Height { get; set; }
        public string Hobby { get; set; }
        public string City { get; set; }

    }
}
