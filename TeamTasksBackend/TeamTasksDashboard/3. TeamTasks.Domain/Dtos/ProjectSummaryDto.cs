namespace _3._TeamTasks.Domain.Dtos
{
    public class ProjectSummaryDto
    {
        public int ProjectId { get; set; }
        public string Name { get; set; } = null!;
        public string ClientName { get; set; } = null!;
        public string? Status { get; set; }
        public int TotalTasks { get; set; }
        public int OpenTasks { get; set; }
        public int CompletedTasks { get; set; }
    }
}
