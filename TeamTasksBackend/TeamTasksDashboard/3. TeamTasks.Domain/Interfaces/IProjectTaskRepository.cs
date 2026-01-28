using _3._TeamTasks.Domain.Models;
using _3._TeamTasks.Domain.Dtos;

namespace _3._TeamTasks.Domain.Interfaces
{
    public interface IProjectTaskRepository : IRepository<ProjectTask>
    {
        Task<List<ProjectTask>> GetProjectTasks(TasksFilterDto filter);
        Task<bool> ProjectHasTasks(int projectId);
        Task CreateTask(CreateTaskDto entity);
    }
}
