namespace _3._TeamTasks.Domain.Dtos
{
    public class CreateDeveloperDto
    {
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Isactive { get; set; }
        public DateTime Createdat { get; set; }
    }
}
