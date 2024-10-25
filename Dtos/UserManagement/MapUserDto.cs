
using System.Text.Json.Serialization;
using personal_trainer_api.Enums;

namespace personal_trainer_api.Dtos.UserManagement
{
    public class MapUserDto
    {
        public int TrainerId { get; set; } = 0;
        public string Email { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EUserRole Role { get; set; }
    }
}