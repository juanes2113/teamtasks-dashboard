using _3._TeamTasks.Domain.Dtos;
using _3._TeamTasks.Domain.Models;

namespace _2._TeamTasks.Application.Interfaces
{
    public interface IProjectTaskApplication
    {
        Task<IEnumerable<ProjectTask>> TasksGetAll();
        Task<ProjectTask> GetByIdTask(int Taskid);
        Task<bool> Create(CreateTaskDto entity);
        Task<bool> Update(UpdateTaskDto entity);
        Task<bool> Delete(int Taskid);
    }
}
