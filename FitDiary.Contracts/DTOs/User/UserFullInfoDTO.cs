namespace FitDiary.Contracts.DTOs.User
{
    public class UserFullInfoDTO
    {
        public UserDTO UserBaseInfo { get; set; }
        public BodyMeasurementsDTO LastBodyMeasurement { get; set; }

        public UserFullInfoDTO()
        {
            UserBaseInfo = new UserDTO();
            LastBodyMeasurement = new BodyMeasurementsDTO();
        }
    }
}
