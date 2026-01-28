using _2._TeamTasks.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Request.Domain.Responses;

namespace _1._TeamTasks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardApplication _dashboardApplication;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(
            IDashboardApplication dashboardApplication,
            ILogger<DashboardController> logger)
        {
            _dashboardApplication = dashboardApplication;
            _logger = logger;
        }

        [HttpGet("workload")]
        public async Task<IActionResult> GetWorkload([FromQuery] int? developerId)
        {
            try
            {
                var result = await _dashboardApplication.GetDevelopersWorkload(developerId);
                return Ok(new ResponseApi
                {
                    IsSuccess = true,
                    Message = "Carga de trabajo obtenida correctamente",
                    Result = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting workload");
                return BadRequest(new ResponseApi
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null!
                });
            }
        }

        [HttpGet("risk")]
        public async Task<IActionResult> GetRiskWorkload([FromQuery] int? developerId)
        {
            try
            {
                var result = await _dashboardApplication.GetDevelopersRiskWorkload(developerId);
                return Ok(new ResponseApi
                {
                    IsSuccess = true,
                    Message = "Riesgo de carga obtenido correctamente",
                    Result = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obtaining load risk");
                return BadRequest(new ResponseApi
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null!
                });
            }
        }
    }
}