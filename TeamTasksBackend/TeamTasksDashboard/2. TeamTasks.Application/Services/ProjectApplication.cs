using _2._TeamTasks.Application.Interfaces;
using _3._TeamTasks.Domain.Interfaces;
using _3._TeamTasks.Domain.Models;
using _3._TeamTasks.Domain.Enums;
using _3._TeamTasks.Domain.Dtos;
using AutoMapper;

namespace _2._TeamTasks.Application.Services
{
    public class ProjectApplication : IProjectApplication
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectApplication(IProjectTaskRepository projectTaskRepository,
            IProjectRepository projectRepository,
            IMapper mapper)
        {
            _projectTaskRepository = projectTaskRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Method that receives the request from the controller to get all projects.
        /// </summary>
        /// <param></param>
        /// <returns> Type: IEnumerable<ProjectSummaryDto> - List with the requested information </returns>
        public async Task<IEnumerable<ProjectSummaryDto>> ProjectsGetAll()
        {
            try
            {
                var projects = await _projectRepository.GetProjectsSummary();
                return projects;
            }
            catch (Exception ex)
            {
                Exception exception = new("Failed" + ex.InnerException + "\n" + ex.Message);
                throw exception;
            }
        }

        /// <summary>
        /// Method that receives the controller's request to get a project by id.
        /// </summary>
        /// <param name="projectId"> Project id to search for </param>
        /// <returns> Type: ProjectSummaryDto - Entity with the requested information </returns>
        public async Task<ProjectSummaryDto> GetByIdProject(int projectId)
        {
            try
            {
                var project = await _projectRepository.GetProjectSummaryById(projectId);
                if (project == null) throw new Exception("No existe proyecto con ese Id.");
                return project;
            }
            catch (Exception ex)
            {
                Exception exception = new("Failed" + ex.InnerException + "\n" + ex.Message);
                throw exception;
            }
        }

        /// <summary>
        /// Method that receives the request from the controller to get the tasks of a project.
        /// </summary>
        /// <param name="filter"> Entity with information to filter </param>
        /// <returns> Type: IEnumerable<ProjectTask> - List with the requested information </returns>
        public async Task<IEnumerable<ProjectTask>> GetTasksByFilter(TasksFilterDto filter)
        {
            try
            {
                var tasks = await _projectTaskRepository.GetProjectTasks(filter);
                if (tasks == null) throw new Exception("No existe tareas con id del proyecto.");
                return tasks;
            }
            catch (Exception ex)
            {
                Exception exception = new("Failed" + ex.InnerException + "\n" + ex.Message);
                throw exception;
            }
        }

        /// <summary>
        /// Method that receives the controller's request to create a new project.
        /// </summary>
        /// <param name="entity"> Type: CreateProjectDto - Entity with the information to be saved </param>
        /// <returns> Type: bool - Indicating whether the operation was successful or not </returns>
        public async Task<bool> Create(CreateProjectDto entity)
        {
            try
            {
                var project = _mapper.Map<Project>(entity);
                project.Status = (int)ProjectStatus.Planned;
                await _projectRepository.Add(project);
                return true;
            }
            catch (Exception ex)
            {
                Exception exception = new("Failed" + ex.InnerException + "\n" + ex.Message);
                throw exception;
            }
        }

        /// <summary>
        /// Method that receives the request from the controller to update a project
        /// </summary>
        /// <param name="entity"> Type: UpdateProjectDto - Entity with the information to update </param>
        /// <returns> Type: bool - Indicating whether the operation was successful or not </returns>
        public async Task<bool> Update(UpdateProjectDto entity)
        {
            try
            {
                var project = await _projectRepository.GetById(entity.Projectid);
                if (project != null)
                {
                    var dataMapper = _mapper.Map<Project>(entity);
                    await _projectRepository.UpdateAsync(dataMapper);
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                Exception exception = new("Failed" + ex.InnerException + "\n" + ex.Message);
                throw exception;
            }
        }

        /// <summary>
        /// Method that receives the request from the controller to delete a project
        /// </summary>
        /// <param name="Projectid"> Type: Int - Id of the entity to delete </param>
        /// <returns> Type: bool - Indicating whether the operation was successful or not </returns>
        public async Task<bool> Delete(int Projectid)
        {
            try
            {
                var existProject = await _projectRepository.GetById(Projectid);
                if (existProject == null) return false;
                var existTasks = await _projectTaskRepository.ProjectHasTasks(Projectid);
                if (existTasks) throw new Exception("El proyecto tiene tareas asociadas, no se puede eliminar.");
                await _projectRepository.Delete(existProject);
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
