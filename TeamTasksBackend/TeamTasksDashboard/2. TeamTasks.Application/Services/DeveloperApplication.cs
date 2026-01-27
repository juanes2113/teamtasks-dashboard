using _2._TeamTasks.Application.Interfaces;
using _3._TeamTasks.Domain.Interfaces;
using _3._TeamTasks.Domain.Models;
using _3._TeamTasks.Domain.Dtos;
using AutoMapper;

namespace _2._TeamTasks.Application.Services
{
    public class DeveloperApplication : IDeveloperApplication
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IMapper _mapper;

        public DeveloperApplication(IDeveloperRepository developerRepository,
            IMapper mapper)
        {
            _developerRepository = developerRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Method that receives the request from the controller to get all developers.
        /// </summary>
        /// <param></param>
        /// <returns> Type: IEnumerable<Developer> - List with the requested information </returns>
        public async Task<IEnumerable<Developer>> DeveloperGetAll()
        {
            try
            {
                var developer = await _developerRepository.GetAll();
                return developer;
            }
            catch (Exception ex)
            {
                Exception exception = new("Failed" + ex.InnerException + "\n" + ex.Message);
                throw exception;
            }
        }

        /// <summary>
        /// Method that receives the controller's request to get a developer by id.
        /// </summary>
        /// <param name="Developerid"> Developer id to search for </param>
        /// <returns> Type: Developer - Entity with the requested information </returns>
        public async Task<Developer> GetByIdDeveloper(int Developerid)
        {
            try
            {
                var developer = await _developerRepository.GetById(Developerid);
                if (developer == null) throw new Exception("No existe desarrollador con ese Id.");
                return developer;
            }
            catch (Exception ex)
            {
                Exception exception = new("Failed" + ex.InnerException + "\n" + ex.Message);
                throw exception;
            }
        }

        /// <summary>
        /// Method that receives the controller's request to create a new developer.
        /// </summary>
        /// <param name="entity"> Type: CreateDeveloperDto - Entity with the information to be saved </param>
        /// <returns> Type: bool - Indicating whether the operation was successful or not </returns>
        public async Task<bool> Create(CreateDeveloperDto entity)
        {
            try
            {
                var developer = _mapper.Map<Developer>(entity);
                var localTime = TimeZoneInfo.FindSystemTimeZoneById("America/Bogota");
                developer.Createdat = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localTime);
                await _developerRepository.Add(developer);
                return true;
            }
            catch (Exception ex)
            {
                Exception exception = new("Failed" + ex.InnerException + "\n" + ex.Message);
                throw exception;
            }
        }

        /// <summary>
        /// Method that receives the request from the controller to update a developer
        /// </summary>
        /// <param name="entity"> Type: UpdateDeveloperDto - Entity with the information to update </param>
        /// <returns> Type: bool - Indicating whether the operation was successful or not </returns>
        public async Task<bool> Update(UpdateDeveloperDto entity)
        {
            try
            {
                var developer = await _developerRepository.GetById(entity.Developerid);
                if (developer != null)
                {
                    var dataMapper = _mapper.Map<Developer>(entity);
                    await _developerRepository.UpdateAsync(dataMapper);
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
        /// Method that receives the request from the controller to delete a developer
        /// </summary>
        /// <param name="Developerid"> Type: Int - Id of the entity to delete </param>
        /// <returns> Type: bool - Indicating whether the operation was successful or not </returns>
        public async Task<bool> Delete(int Developerid)
        {
            try
            {
                var existDeveloper = await _developerRepository.GetById(Developerid);
                if (existDeveloper == null) return false;
                await _developerRepository.Delete(existDeveloper);
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