using _3._TeamTasks.Domain.Dtos;
using _3._TeamTasks.Domain.Models;

namespace _2._TeamTasks.Application.Interfaces
{
    public interface IDeveloperApplication
    {
        Task<IEnumerable<Developer>> DeveloperGetAll();
        Task<Developer> GetByIdDeveloper(int Developerid);
        Task<bool> Create(CreateDeveloperDto entity);
        Task<bool> Update(UpdateDeveloperDto entity);
        Task<bool> Delete(int Developerid);
    }
}
