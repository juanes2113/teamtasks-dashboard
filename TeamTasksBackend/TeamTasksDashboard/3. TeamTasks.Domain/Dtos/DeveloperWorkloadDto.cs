namespace _3._TeamTasks.Domain.Dtos
{
    public class DeveloperWorkloadDto
    {
        public int DeveloperId { get; set; }
        public string DeveloperName { get; set; } = null!;
        public int OpenTasksCount { get; set; }
        public double? AverageEstimatedComplexity { get; set; }
    }
}
