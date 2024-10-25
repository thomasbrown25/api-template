

namespace personal_trainer_api.Models
{
    public class Client
    {
        public int Id { get; set; }
        public int? UserId { get; set; } = 0;
        public int TrainerId { get; set; } = 0;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Height { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public string FitnessLevel { get; set; } = string.Empty;
        public string FitnessGoals { get; set; } = string.Empty;
        public string WeightGoal { get; set; } = string.Empty;
        public DateOnly? DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public DateTime LastVisited { get; set; } = DateTime.Now;
    }
}