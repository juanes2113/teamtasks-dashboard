using _3._TeamTasks.Domain.Models;
using _3._TeamTasks.Domain.Dtos;

namespace _3._TeamTasks.Domain.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<List<ProjectSummaryDto>> GetProjectsSummary();
        Task<ProjectSummaryDto?> GetProjectSummaryById(int projectId);
    }
}
