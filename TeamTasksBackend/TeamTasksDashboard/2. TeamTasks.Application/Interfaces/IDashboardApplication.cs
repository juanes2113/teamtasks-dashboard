using _3._TeamTasks.Domain.Dtos;

namespace _2._TeamTasks.Application.Interfaces
{
    public interface IDashboardApplication
    {
        Task<IEnumerable<DeveloperWorkloadDto>> GetDevelopersWorkload(int? developerId);
        Task<IEnumerable<DeveloperRiskWorkloadDto>> GetDevelopersRiskWorkload(int? developerId);
    }
}
