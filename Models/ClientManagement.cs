

namespace personal_trainer_api.Models
{
    public class UserManagement
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public int ClientId { get; set; }
        public bool Enabled { get; set; } = true;
    }
}