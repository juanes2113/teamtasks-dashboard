namespace _3._TeamTasks.Domain.Dtos
{
    public class DeveloperRiskWorkloadDto
    {
        public int DeveloperId { get; set; }
        public string DeveloperName { get; set; } = null!;
        public int OpenTasksCount { get; set; }
        public double AvgDelayDays { get; set; }
        public DateOnly? NearestDueDate { get; set; }
        public DateOnly? LatestDueDate { get; set; }
        public DateTime? PredictedCompletionDate { get; set; }
        public bool HighRiskFlag { get; set; }
    }
}
