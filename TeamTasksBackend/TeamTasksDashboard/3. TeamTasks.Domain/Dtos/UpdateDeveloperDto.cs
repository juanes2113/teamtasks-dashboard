namespace _3._TeamTasks.Domain.Dtos
{
    public class UpdateDeveloperDto
    {
        public int Developerid { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Isactive { get; set; }
    }
}
