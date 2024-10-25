namespace personal_trainer_api.Dtos.Client
{
    public class UpdateClientDto
    {
        public int Id { get; set; }
        public int TrainerId { get; set; } = 0;
        public string Email { get; set; } = string.Empty;
    }
}