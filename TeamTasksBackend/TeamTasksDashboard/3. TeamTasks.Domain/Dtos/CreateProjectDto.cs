namespace _3._TeamTasks.Domain.Dtos
{
    public class CreateProjectDto
    {
        public string Name { get; set; } = null!;
        public string Clientname { get; set; } = null!;
        public DateOnly Startdate { get; set; }
        public DateOnly? Enddate { get; set; }
    }
}
