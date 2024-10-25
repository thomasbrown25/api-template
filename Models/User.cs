
using personal_trainer_api.Enums;

namespace personal_trainer_api.Models
{
    public class User
    {
        public int Id { get; set; }
        public int SettingId { get; set; }
        public int? TrainerId { get; set; } = 0;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Height { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public EUserRole Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string? AccessToken { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}