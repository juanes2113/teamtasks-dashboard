using _2._TeamTasks.Application.Interfaces;
using _3._TeamTasks.Domain.Interfaces;
using _3._TeamTasks.Domain.Models;
using _3._TeamTasks.Domain.Dtos;
using AutoMapper;

namespace _2._TeamTasks.Application.Services
{
    public class ProjectTaskApplication : IProjectTaskApplication
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IMapper _mapper;

        public ProjectTaskApplication(IProjectTaskRepository projectTaskRepository,
            IMapper mapper)
        {
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Method that receives the request from the controller to get all tasks.
        /// </summary>
        /// <param></param>
        /// <returns> Type: IEnumerable<ProjectTask> - List with the requested information </returns>
        public async Task<IEnumerable<ProjectTask>> TasksGetAll()
        {
            try
            {
                var tasks = await _projectTaskRepository.GetAll();
                return tasks;
            }
            catch (Exception ex)
            {
                Exception exception = new("Failed" + ex.InnerException + "\n" + ex.Message);
                throw exception;
            }
        }

        /// <summary>
        /// Method that receives the controller's request to get a task by id.
        /// </summary>
        /// <param name="Taskid"> Task id to search for </param>
        /// <returns> Type: ProjectTask - Entity with the requested information </returns>
        public async Task<ProjectTask> GetByIdTask(int Taskid)
        {
            try
            {
                var task = await _projectTaskRepository.GetById(Taskid);
                if (task == null) throw new Exception("No existe tareas con ese Id.");
                return task;
            }
            catch (Exception ex)
            {
                Exception exception = new("Failed" + ex.InnerException + "\n" + ex.Message);
                throw exception;
            }
        }

        /// <summary>
        /// Method that receives the controller's request to create a new task.
        /// </summary>
        /// <param name="entity"> Type: CreateTaskDto - Entity with the information to be saved </param>
        /// <returns> Type: bool - Indicating whether the operation was successful or not </returns>
        public async Task<bool> Create(CreateTaskDto entity)
        {
            try
            {
                await _projectTaskRepository.CreateTask(entity);
                return true;
            }
            catch (Exception ex)
            {
                Exception exception = new("Failed" + ex.InnerException + "\n" + ex.Message);
                throw exception;
            }
        }

        /// <summary>
        /// Method that receives the request from the controller to update a task
        /// </summary>
        /// <param name="entity"> Type: UpdateTaskDto - Entity with the information to update </param>
        /// <returns> Type: bool - Indicating whether the operation was successful or not </returns>
        public async Task<bool> Update(UpdateTaskDto entity)
        {
            try
            {
                var task = await _projectTaskRepository.GetById(entity.Taskid);
                if (task == null) return false;
                task.Title = entity.Title;
                task.Description = entity.Description;
                task.Assigneeid = entity.Assigneeid;
                task.Status = entity.Status;
                task.Estimatedcomplexity = entity.Estimatedcomplexity;
                task.Completiondate = entity.Completiondate;
                await _projectTaskRepository.UpdateAsync(task);
                return true;
            }
            catch (Exception ex)
            {
                Exception exception = new("Failed" + ex.InnerException + "\n" + ex.Message);
                throw exception;
            }
        }

        /// <summary>
        /// Method that receives the request from the controller to delete a task
        /// </summary>
        /// <param name="Taskid"> Type: Int - Id of the entity to delete </param>
        /// <returns> Type: bool - Indicating whether the operation was successful or not </returns>
        public async Task<bool> Delete(int Taskid)
        {
            try
            {
                var existDeveloper = await _projectTaskRepository.GetById(Taskid);
                if (existDeveloper == null) return false;
                await _projectTaskRepository.Delete(existDeveloper);
                return true;
            }
            catch (Exception ex)
            {
                Exception exception = new("Failed" + ex.InnerException + "\n" + ex.Message);
                throw exception;
            }
        }
    }
}
