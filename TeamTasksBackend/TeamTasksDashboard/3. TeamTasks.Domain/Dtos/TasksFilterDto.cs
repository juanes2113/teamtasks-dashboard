namespace _3._TeamTasks.Domain.Dtos
{
    public class TasksFilterDto
    {
        public int Projectid { get; set; }
        public int? Status { get; set; }
        public int? AssigneeId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
