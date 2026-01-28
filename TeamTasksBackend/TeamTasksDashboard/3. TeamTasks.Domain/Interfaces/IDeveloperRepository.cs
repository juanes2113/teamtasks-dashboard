using _3._TeamTasks.Domain.Dtos;
using _3._TeamTasks.Domain.Models;

namespace _3._TeamTasks.Domain.Interfaces
{
    public interface IDeveloperRepository : IRepository<Developer>
    {
        Task<List<DeveloperWorkloadDto>> GetDevelopersWorkload(int? developerId);
        Task<List<DeveloperRiskWorkloadDto>> GetDevelopersRiskWorkload(int? developerId);
    }
}
