

using System.Text.Json.Serialization;
using template_api.Enums;

namespace template_api.Dtos.User
{
    public class LoadUserDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TrainerId { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string JWTToken { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Height { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public string FitnessLevel { get; set; } = string.Empty;
        public string FitnessGoals { get; set; } = string.Empty;
        public DateOnly? DateOfBirth { get; set; }
        public string WeightGoal { get; set; } = string.Empty;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EUserRole Role { get; set; } = EUserRole.Invalid;
        public DateTime LastVisited { get; set; } = DateTime.MinValue;
    }
}