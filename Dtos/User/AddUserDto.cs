
using System.Text.Json.Serialization;
using personal_trainer_api.Enums;

namespace personal_trainer_api.Dtos.User
{
    public class AddUserDto
    {
        public string RegistrationCode { get; set; } = string.Empty;
        public int TrainerId { get; set; } = 0;
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Height { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public string FitnessLevel { get; set; } = string.Empty;
        public string FitnessGoals { get; set; } = string.Empty;
        public string WeightGoal { get; set; } = string.Empty;
        public string BirthMonth { get; set; } = string.Empty;
        public string BirthDay { get; set; } = string.Empty;
        public string BirthYear { get; set; } = string.Empty;
        public DateOnly? DateOfBirth { get; set; } = null;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EUserRole Role { get; set; } = EUserRole.Invalid;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
    }
}