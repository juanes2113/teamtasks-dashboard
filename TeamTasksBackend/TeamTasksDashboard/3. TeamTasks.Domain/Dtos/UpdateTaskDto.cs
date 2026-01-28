namespace _3._TeamTasks.Domain.Dtos
{
    public class UpdateTaskDto
    {
        public int Taskid { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int Assigneeid { get; set; }
        public int Status { get; set; }
        public int? Estimatedcomplexity { get; set; }
        public DateOnly? Completiondate { get; set; }
    }
}
