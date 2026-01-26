namespace _3._TeamTasks.Domain.Models
{
    public partial class ProjectTask
    {
        public int Taskid { get; set; }
        public int Projectid { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int Assigneeid { get; set; }
        public int? Estimatedcomplexity { get; set; }
        public DateOnly Duedate { get; set; }
        public DateOnly? Completiondate { get; set; }
        public DateTime Createdat { get; set; }
        public virtual Developer Assignee { get; set; } = null!;
        public virtual Project Project { get; set; } = null!;
    }
}
