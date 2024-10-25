namespace personal_trainer_api.Dtos.Workout
{
    public class AddWorkoutDto
    {
        public int TrainerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string TargetMuscle { get; set; } = string.Empty;
        public string WorkoutLevel { get; set; } = string.Empty;
        public int Duration { get; set; } = 0;
    }
}