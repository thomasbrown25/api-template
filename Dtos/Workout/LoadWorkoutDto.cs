namespace personal_trainer_api.Dtos.Workout
{
    public class LoadWorkoutDto
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}