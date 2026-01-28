using _4._TeamTasks.Infrastructure.Data;
using _3._TeamTasks.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using _3._TeamTasks.Domain.Models;
using _3._TeamTasks.Domain.Enums;
using _3._TeamTasks.Domain.Dtos;

namespace _4._TeamTasks.Infrastructure.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly TeamTasksDbContext _context;
        public ProjectRepository(TeamTasksDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that retrieves all records from the Projects table.
        /// </summary>
        /// <param> </param>
        /// <returns> Type: List<ProjectSummaryDto> - List with the requested information </returns>
        public async Task<List<ProjectSummaryDto>> GetProjectsSummary()
        {
            return await _context.Projects
                .AsNoTracking()
                .Select(p => new ProjectSummaryDto
                {
                    ProjectId = p.Projectid,
                    Name = p.Name,
                    ClientName = p.Clientname,
                    Status = p.Status == (int)ProjectStatus.Planned ? "Planned"
                        : p.Status == (int)ProjectStatus.InProgress ? "InProgress"
                        : p.Status == (int)ProjectStatus.Completed ? "Completed"
                        : "Unknown",
                    TotalTasks = p.ProjectTasks.Count(),
                    OpenTasks = p.ProjectTasks.Count(t => t.Status != (int)TasksStatus.Completed),
                    CompletedTasks = p.ProjectTasks.Count(t => t.Status == (int)TasksStatus.Completed)
                })
                .ToListAsync();
        }

        /// <summary>
        /// Method that retrieves a record from the Projects table by its id.
        /// </summary>
        /// <param name="projectId"> Project Id to search for </param>
        /// <returns> Type: ProjectSummaryDto - Entity with the requested information </returns>
        public async Task<ProjectSummaryDto?> GetProjectSummaryById(int projectId)
        {
            return await _context.Projects
                .AsNoTracking()
                .Where(p => p.Projectid == projectId)
                .Select(p => new ProjectSummaryDto
                {
                    ProjectId = p.Projectid,
                    Name = p.Name,
                    ClientName = p.Clientname,
                    Status = p.Status == (int)ProjectStatus.Planned ? "Planned"
                        : p.Status == (int)ProjectStatus.InProgress ? "InProgress"
                        : p.Status == (int)ProjectStatus.Completed ? "Completed"
                        : "Unknown",
                    TotalTasks = p.ProjectTasks.Count(),
                    OpenTasks = p.ProjectTasks.Count(t => t.Status != (int)TasksStatus.Completed),
                    CompletedTasks = p.ProjectTasks.Count(t => t.Status == (int)TasksStatus.Completed)
                })
                .FirstOrDefaultAsync();
        }
    }
}
