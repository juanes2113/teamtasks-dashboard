namespace _3._TeamTasks.Domain.Models
{
    public partial class Project
    {
        public int Projectid { get; set; }
        public string Name { get; set; } = null!;
        public string Clientname { get; set; } = null!;
        public DateOnly Startdate { get; set; }
        public DateOnly? Enddate { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
    }
}
