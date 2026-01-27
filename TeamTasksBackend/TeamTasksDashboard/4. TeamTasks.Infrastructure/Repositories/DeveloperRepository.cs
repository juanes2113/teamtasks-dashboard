using _4._TeamTasks.Infrastructure.Data;
using _3._TeamTasks.Domain.Interfaces;
using _3._TeamTasks.Domain.Models;

namespace _4._TeamTasks.Infrastructure.Repositories
{
    public class DeveloperRepository : Repository<Developer>, IDeveloperRepository
    {
        private readonly TeamTasksDbContext _context;
        public DeveloperRepository(TeamTasksDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
