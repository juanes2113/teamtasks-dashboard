namespace _3._TeamTasks.Domain.Dtos
{
    public class CreateTaskDto
    {
        public int Projectid { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int Assigneeid { get; set; }
        public int Status { get; set; }
        public int Priority { get; set; }
        public int? Estimatedcomplexity { get; set; }
        public DateOnly Duedate { get; set; }
    }
}
