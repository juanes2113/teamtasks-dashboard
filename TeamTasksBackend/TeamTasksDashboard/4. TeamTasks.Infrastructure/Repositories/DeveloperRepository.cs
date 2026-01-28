using _4._TeamTasks.Infrastructure.Data;
using _3._TeamTasks.Domain.Interfaces;
using _3._TeamTasks.Domain.Models;
using _3._TeamTasks.Domain.Dtos;
using Microsoft.EntityFrameworkCore;
using _3._TeamTasks.Domain.Enums;

namespace _4._TeamTasks.Infrastructure.Repositories
{
    public class DeveloperRepository : Repository<Developer>, IDeveloperRepository
    {
        private readonly TeamTasksDbContext _context;
        public DeveloperRepository(TeamTasksDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to return the load summary by developer.
        /// </summary>
        /// <param name="developerId"> Developer id to search for </param>
        /// <returns> Type: List<DeveloperWorkloadDto> - List with the requested information </returns>
        public async Task<List<DeveloperWorkloadDto>> GetDevelopersWorkload(int? developerId)
        {
            var query = _context.Developers
                .AsNoTracking()
                .Where(d => d.Isactive);
            if (developerId.HasValue)
            {
                query = query.Where(d => d.Developerid == developerId.Value);
            }
            return await query
                .Select(d => new DeveloperWorkloadDto
                {
                    DeveloperId = d.Developerid,
                    DeveloperName = d.Firstname + " " + d.Lastname,
                    OpenTasksCount = d.ProjectTasks
                        .Count(t => t.Status != 3),
                    AverageEstimatedComplexity = d.ProjectTasks
                        .Where(t => t.Status != 3 && t.Estimatedcomplexity.HasValue)
                        .Average(t => (double?)t.Estimatedcomplexity)
                })
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves workload and risk indicators per developer, including open tasks count,
        /// average delay in completed tasks, predicted completion date and risk flag.
        /// If developerId is provided, returns information only for that developer.
        /// </summary>
        /// <param name="developerId"> Developer id to search for </param>
        /// <returns> Type: List<DeveloperRiskWorkloadDto> - List with the requested information </returns>
        public async Task<List<DeveloperRiskWorkloadDto>> GetDevelopersRiskWorkload(int? developerId)
        {
            var query = _context.Developers
                .AsNoTracking()
                .Where(d => d.Isactive);
            if (developerId.HasValue)
            {
                query = query.Where(d => d.Developerid == developerId.Value);
            }
            var developers = await query
                .Select(d => new
                {
                    Developer = d,
                    CompletedTasks = d.ProjectTasks
                        .Where(t => t.Status == (int)TasksStatus.Completed && t.Completiondate.HasValue)
                        .ToList(),
                    OpenTasks = d.ProjectTasks
                        .Where(t => t.Status != (int)TasksStatus.Completed)
                        .ToList()
                })
                .ToListAsync();
            return developers.Select(x =>
            {
                var avgDelayDays = x.CompletedTasks.Any()
                    ? x.CompletedTasks.Average(t =>
                        Math.Max((t.Completiondate!.Value.DayNumber - t.Duedate.DayNumber), 0))
                    : 0;
                var latestDueDate = x.OpenTasks.Any()
                    ? x.OpenTasks.Max(t => t.Duedate)
                    : (DateOnly?)null;
                var predictedCompletionDate = latestDueDate.HasValue
                    ? latestDueDate.Value.ToDateTime(TimeOnly.MinValue).AddDays(avgDelayDays)
                    : (DateTime?)null;
                return new DeveloperRiskWorkloadDto
                {
                    DeveloperId = x.Developer.Developerid,
                    DeveloperName = x.Developer.Firstname + " " + x.Developer.Lastname,
                    OpenTasksCount = x.OpenTasks.Count(),
                    AvgDelayDays = avgDelayDays,
                    NearestDueDate = x.OpenTasks.Any() ? x.OpenTasks.Min(t => t.Duedate) : null,
                    LatestDueDate = latestDueDate,
                    PredictedCompletionDate = predictedCompletionDate,
                    HighRiskFlag = predictedCompletionDate.HasValue
                                   && latestDueDate.HasValue
                                   && predictedCompletionDate > latestDueDate.Value.ToDateTime(TimeOnly.MinValue)
                };
            }).ToList();
        }
    }
}
