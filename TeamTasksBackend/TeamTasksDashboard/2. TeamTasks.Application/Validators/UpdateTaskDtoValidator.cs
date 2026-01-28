using _3._TeamTasks.Domain.Dtos;
using FluentValidation;

namespace _2._TeamTasks.Application.Validators
{
    public class UpdateTaskDtoValidator : AbstractValidator<UpdateTaskDto>
    {
        public UpdateTaskDtoValidator()
        {
            RuleFor(x => x.Taskid)
                .NotEmpty()
                .WithMessage("El id de la tarea es requerido");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("El título de la tarea es requerido")
                .MaximumLength(200).WithMessage("La longitud máxima del título es de 200 caracteres");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("La descripción no puede superar los 500 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Assigneeid)
                .NotEmpty()
                .WithMessage("El id del asignado es requerido");

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("El estado es requerido");

            RuleFor(x => x.Estimatedcomplexity)
                .GreaterThan(0).WithMessage("La complejidad estimada debe ser mayor a 0")
                .When(x => x.Estimatedcomplexity.HasValue);

            RuleFor(x => x.Completiondate)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .When(x => x.Completiondate.HasValue)
                .WithMessage("La fecha de finalización no puede ser mayor a la fecha actual");
        }
    }
}