using _2._TeamTasks.Application.Interfaces;
using _3._TeamTasks.Domain.Interfaces;
using _3._TeamTasks.Domain.Dtos;

namespace _2._TeamTasks.Application.Services
{
    public class DashboardApplication : IDashboardApplication
    {
        private readonly IDeveloperRepository _developerRepository;

        public DashboardApplication(IDeveloperRepository developerRepository)
        {
            _developerRepository = developerRepository;
        }

        /// <summary>
        /// Method that receives the request from the controller to return the load summary by developer.
        /// </summary>
        /// <param name="developerId"> Developer id to search for </param>
        /// <returns> Type: IEnumerable<DeveloperWorkloadDto> - List with the requested information </returns>
        public async Task<IEnumerable<DeveloperWorkloadDto>> GetDevelopersWorkload(int? developerId)
        {
            try
            {
                var developer = await _developerRepository.GetDevelopersWorkload(developerId);
                if (developer == null) throw new Exception("No existen desarrolladores.");
                return developer;
            }
            catch (Exception ex)
            {
                Exception exception = new("Failed" + ex.InnerException + "\n" + ex.Message);
                throw exception;
            }
        }

        /// <summary>
        /// Method that receives the controller's request to return the risk of delay for your open tasks.
        /// </summary>
        /// <param name="developerId"> Developer id to search for </param>
        /// <returns> Type: IEnumerable<DeveloperRiskWorkloadDto> - List with the requested information </returns>
        public async Task<IEnumerable<DeveloperRiskWorkloadDto>> GetDevelopersRiskWorkload(int? developerId)
        {
            try
            {
                var developer = await _developerRepository.GetDevelopersRiskWorkload(developerId);
                if (developer == null) throw new Exception("No existen desarrolladores.");
                return developer;
            }
            catch (Exception ex)
            {
                Exception exception = new("Failed" + ex.InnerException + "\n" + ex.Message);
                throw exception;
            }
        }
    }
}
