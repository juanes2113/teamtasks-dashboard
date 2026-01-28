using _2._TeamTasks.Application.Interfaces;
using _3._TeamTasks.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Request.Domain.Responses;
using FluentValidation;

namespace _1._TeamTasks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IProjectTaskApplication _projectTaskApplication;
        private readonly IValidator<CreateTaskDto> _createValidator;
        private readonly IValidator<UpdateTaskDto> _updateValidator;
        private readonly ILogger<DeveloperController> _logger;

        public ProjectTaskController(IProjectTaskApplication projectTaskApplication,
            IValidator<CreateTaskDto> createValidator,
            IValidator<UpdateTaskDto> updateValidator,
            ILogger<DeveloperController> logger)
        {
            _projectTaskApplication = projectTaskApplication;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tasks = await _projectTaskApplication.TasksGetAll();
                return Ok(new ResponseApi
                {
                    IsSuccess = true,
                    Message = "Tareas obtenidos correctamente",
                    Result = tasks
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all tasks");
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
                var task = await _projectTaskApplication.GetByIdTask(id);
                return Ok(new ResponseApi
                {
                    IsSuccess = true,
                    Message = "Tarea encontrada",
                    Result = task
                });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"Task with id {id} not found");
                return NotFound(new ResponseApi
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null!
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
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
                var success = await _projectTaskApplication.Create(dto);
                if (success)
                    return Ok(new ResponseApi
                    {
                        IsSuccess = true,
                        Message = "Tarea creada correctamente",
                        Result = null!
                    });
                else
                    return BadRequest(new ResponseApi
                    {
                        IsSuccess = false,
                        Message = "No se pudo crear la tarea",
                        Result = null!
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating task");
                return StatusCode(500, new ResponseApi
                {
                    IsSuccess = false,
                    Message = "Internal server error",
                    Result = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTaskDto dto)
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
                var success = await _projectTaskApplication.Update(dto);
                if (success)
                    return Ok(new ResponseApi
                    {
                        IsSuccess = true,
                        Message = "Tarea actualizada correctamente",
                        Result = null!
                    });
                else
                    return NotFound(new ResponseApi
                    {
                        IsSuccess = false,
                        Message = "Tarea no encontrada",
                        Result = null!
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task");
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
                var success = await _projectTaskApplication.Delete(id);
                if (success)
                    return Ok(new ResponseApi
                    {
                        IsSuccess = true,
                        Message = "Tarea eliminada correctamente",
                        Result = null!
                    });
                else
                    return NotFound(new ResponseApi
                    {
                        IsSuccess = false,
                        Message = "Tarea no encontrado",
                        Result = null!
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting task");
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
