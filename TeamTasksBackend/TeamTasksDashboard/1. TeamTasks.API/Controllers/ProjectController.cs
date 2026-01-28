using _2._TeamTasks.Application.Interfaces;
using _3._TeamTasks.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Request.Domain.Responses;
using FluentValidation;

namespace _1._TeamTasks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IValidator<CreateProjectDto> _createValidator;
        private readonly IValidator<UpdateProjectDto> _updateValidator;
        private readonly IValidator<TasksFilterDto> _filterValidator;
        private readonly IProjectApplication _projectApplication;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(IValidator<CreateProjectDto> createValidator,
            IValidator<UpdateProjectDto> updateValidator,
            IValidator<TasksFilterDto> filterValidator,
            IProjectApplication projectApplication,
            ILogger<ProjectController> logger)
        {
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _filterValidator = filterValidator;
            _projectApplication = projectApplication;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var projects = await _projectApplication.ProjectsGetAll();
                return Ok(new ResponseApi
                {
                    IsSuccess = true,
                    Message = "Proyectos obtenidos correctamente",
                    Result = projects
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all projects");
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
                var project = await _projectApplication.GetByIdProject(id);
                return Ok(new ResponseApi
                {
                    IsSuccess = true,
                    Message = "Proyecto encontrado",
                    Result = project
                });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"Project with id {id} not found");
                return NotFound(new ResponseApi
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null!
                });
            }
        }

        [HttpPost("tasks")]
        public async Task<IActionResult> GetTasksByFilter([FromBody] TasksFilterDto filter)
        {
            var validationResult = await _filterValidator.ValidateAsync(filter);
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
                var tasks = await _projectApplication.GetTasksByFilter(filter);
                if (tasks == null || !tasks.Any())
                {
                    return NotFound(new ResponseApi
                    {
                        IsSuccess = false,
                        Message = "No se encontraron tareas para el proyecto especificado",
                        Result = null!
                    });
                }
                return Ok(new ResponseApi
                {
                    IsSuccess = true,
                    Message = "Tareas obtenidas correctamente",
                    Result = tasks
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting filtered tasks");
                return StatusCode(500, new ResponseApi
                {
                    IsSuccess = false,
                    Message = "Internal server error",
                    Result = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectDto dto)
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
                var success = await _projectApplication.Create(dto);
                if (success)
                    return Ok(new ResponseApi
                    {
                        IsSuccess = true,
                        Message = "Proyecto creado correctamente",
                        Result = null!
                    });
                else
                    return BadRequest(new ResponseApi
                    {
                        IsSuccess = false,
                        Message = "No se pudo crear el proyecto",
                        Result = null!
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating project");
                return StatusCode(500, new ResponseApi
                {
                    IsSuccess = false,
                    Message = "Internal server error",
                    Result = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProjectDto dto)
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
                var success = await _projectApplication.Update(dto);
                if (success)
                    return Ok(new ResponseApi
                    {
                        IsSuccess = true,
                        Message = "Proyecto actualizado correctamente",
                        Result = null!
                    });
                else
                    return NotFound(new ResponseApi
                    {
                        IsSuccess = false,
                        Message = "Proyecto no encontrado",
                        Result = null!
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating project");
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
                var success = await _projectApplication.Delete(id);
                if (success)
                    return Ok(new ResponseApi
                    {
                        IsSuccess = true,
                        Message = "Proyecto eliminado correctamente",
                        Result = null!
                    });
                else
                    return NotFound(new ResponseApi
                    {
                        IsSuccess = false,
                        Message = "Proyecto no encontrado",
                        Result = null!
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting project");
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
