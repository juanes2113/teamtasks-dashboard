using _2._TeamTasks.Application.Interfaces;
using _3._TeamTasks.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Request.Domain.Responses;
using FluentValidation;

namespace _1._TeamTasks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeveloperController : ControllerBase
    {
        private readonly IValidator<CreateDeveloperDto> _createValidator;
        private readonly IValidator<UpdateDeveloperDto> _updateValidator;
        private readonly IDeveloperApplication _developerApplication;
        private readonly ILogger<DeveloperController> _logger;

        public DeveloperController(IValidator<CreateDeveloperDto> createValidator,
            IValidator<UpdateDeveloperDto> updateValidator,
            IDeveloperApplication developerApplication,
            ILogger<DeveloperController> logger)
        {
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _developerApplication = developerApplication;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var developers = await _developerApplication.DeveloperGetAll();
                return Ok(new ResponseApi
                {
                    IsSuccess = true,
                    Message = "Desarrolladores obtenidos correctamente",
                    Result = developers
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all developers");
                return StatusCode(500, new ResponseApi
                {
                    IsSuccess = false,
                    Message = "Internal server error",
                    Result = ex.Message
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var developer = await _developerApplication.GetByIdDeveloper(id);
                return Ok(new ResponseApi
                {
                    IsSuccess = true,
                    Message = "Desarrollador encontrado",
                    Result = developer
                });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"Developer with id {id} not found");
                return NotFound(new ResponseApi
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null!
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDeveloperDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseApi
                {
                    IsSuccess = false,
                    Message = "La validación ha fallado",
                    Result = validationResult.Errors
                });
            }
            try
            {
                var success = await _developerApplication.Create(dto);
                if (success)
                    return Ok(new ResponseApi
                    {
                        IsSuccess = true,
                        Message = "Desarrollador creado correctamente",
                        Result = null!
                    });
                else
                    return BadRequest(new ResponseApi
                    {
                        IsSuccess = false,
                        Message = "No se pudo crear el desarrollador",
                        Result = null!
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating developer");
                return StatusCode(500, new ResponseApi
                {
                    IsSuccess = false,
                    Message = "Internal server error",
                    Result = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateDeveloperDto dto)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseApi
                {
                    IsSuccess = false,
                    Message = "La validación ha fallado",
                    Result = validationResult.Errors
                });
            }
            try
            {
                var success = await _developerApplication.Update(dto);
                if (success)
                    return Ok(new ResponseApi
                    {
                        IsSuccess = true,
                        Message = "Desarrollador actualizado correctamente",
                        Result = null!
                    });
                else
                    return NotFound(new ResponseApi
                    {
                        IsSuccess = false,
                        Message = "Desarrollador no encontrado",
                        Result = null!
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating developer");
                return StatusCode(500, new ResponseApi
                {
                    IsSuccess = false,
                    Message = "Internal server error",
                    Result = ex.Message
                });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _developerApplication.Delete(id);
                if (success)
                    return Ok(new ResponseApi
                    {
                        IsSuccess = true,
                        Message = "Desarrollador eliminado correctamente",
                        Result = null!
                    });
                else
                    return NotFound(new ResponseApi
                    {
                        IsSuccess = false,
                        Message = "Desarrollador no encontrado",
                        Result = null!
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting developer");
                return StatusCode(500, new ResponseApi
                {
                    IsSuccess = false,
                    Message = "Internal server error",
                    Result = ex.Message
                });
            }
        }
    }
}
