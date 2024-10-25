namespace personal_trainer_api.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public int TrainerId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
    }
}