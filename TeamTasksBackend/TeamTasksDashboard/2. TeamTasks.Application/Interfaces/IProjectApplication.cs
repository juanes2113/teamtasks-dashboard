using _3._TeamTasks.Domain.Dtos;
using _3._TeamTasks.Domain.Models;

namespace _2._TeamTasks.Application.Interfaces
{
    public interface IProjectApplication
    {
        Task<IEnumerable<ProjectSummaryDto>> ProjectsGetAll();
        Task<ProjectSummaryDto> GetByIdProject(int projectId);
        Task<IEnumerable<ProjectTask>> GetTasksByFilter(TasksFilterDto filter);
        Task<bool> Create(CreateProjectDto entity);
        Task<bool> Update(UpdateProjectDto entity);
        Task<bool> Delete(int Projectid);
    }
}
