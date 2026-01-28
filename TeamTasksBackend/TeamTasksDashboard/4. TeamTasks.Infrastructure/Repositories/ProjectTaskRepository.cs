using _4._TeamTasks.Infrastructure.Data;
using _3._TeamTasks.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using _3._TeamTasks.Domain.Models;
using _3._TeamTasks.Domain.Enums;
using _3._TeamTasks.Domain.Dtos;

namespace _4._TeamTasks.Infrastructure.Repositories
{
    public class ProjectTaskRepository : Repository<ProjectTask>, IProjectTaskRepository
    {
        private readonly TeamTasksDbContext _context;
        public ProjectTaskRepository(TeamTasksDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that retrieves all records from the Tasks table by ProjectId
        /// </summary>
        /// <param name="filter"> Entity with information to filter </param>
        /// <returns> Type: List<ProjectTask> - List with the requested information </returns>
        public async Task<List<ProjectTask>> GetProjectTasks(TasksFilterDto filter)
        {
            int pageSize = (filter.PageSize > 0) ? filter.PageSize : 10;
            int page = (filter.Page > 0) ? filter.Page : 1;
            var query = _context.Tasks
                .AsNoTracking()
                .Where(t => t.Projectid == filter.Projectid);
            if (filter.Status.HasValue)
            {
                query = query.Where(t => t.Status == filter.Status.Value);
            }
            if (filter.AssigneeId.HasValue)
            {
                query = query.Where(t => t.Assigneeid == filter.AssigneeId.Value);
            }
            int skip = (page - 1) * pageSize;
            return await query
                .OrderBy(t => t.Duedate)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }

        /// <summary>
        /// Method that checks if a project has associated tasks.
        /// </summary>
        /// <param name="projectId"> Project Id </param>
        /// <returns>  Type: bool - True if it has tasks, False if not </returns>
        public async Task<bool> ProjectHasTasks(int projectId)
        {
            return await _context.Tasks
                .AsNoTracking()
                .AnyAsync(t => t.Projectid == projectId);
        }

        /// <summary>
        /// Creates a new task by executing the InsertTask stored procedure..
        /// </summary>
        /// <param name="entity"> Entity with the information to be saved </param>
        /// <returns>  Type: - </returns>
        public async Task CreateTask(CreateTaskDto entity)
        {
            await _context.Database.ExecuteSqlRawAsync(
                """
                SELECT InsertTask(
                    @p_projectId,
                    @p_title,
                    @p_description,
                    @p_assigneeId,
                    @p_status,
                    @p_priority,
                    @p_complexity,
                    @p_dueDate
                )
                """,
                new Npgsql.NpgsqlParameter("p_projectId", entity.Projectid),
                new Npgsql.NpgsqlParameter("p_title", entity.Title),
                new Npgsql.NpgsqlParameter("p_description", (object?)entity.Description ?? DBNull.Value),
                new Npgsql.NpgsqlParameter("p_assigneeId", entity.Assigneeid),
                new Npgsql.NpgsqlParameter("p_status", (int)TasksStatus.ToDo),
                new Npgsql.NpgsqlParameter("p_priority", entity.Priority),
                new Npgsql.NpgsqlParameter("p_complexity", entity.Estimatedcomplexity),
                new Npgsql.NpgsqlParameter("p_dueDate", entity.Duedate)
            );
        }
    }
}