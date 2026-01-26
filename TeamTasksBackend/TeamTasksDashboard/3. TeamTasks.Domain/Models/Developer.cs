namespace _3._TeamTasks.Domain.Models
{
    public partial class Developer
    {
        public int Developerid { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Isactive { get; set; }
        public DateTime Createdat { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
    }
}
