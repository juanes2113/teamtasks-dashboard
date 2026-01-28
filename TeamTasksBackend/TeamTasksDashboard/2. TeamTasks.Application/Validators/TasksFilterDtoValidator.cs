using _3._TeamTasks.Domain.Dtos;
using FluentValidation;

namespace _2._TeamTasks.Application.Validators
{
    public class TasksFilterDtoValidator : AbstractValidator<TasksFilterDto>
    {
        public TasksFilterDtoValidator()
        {
            RuleFor(x => x.Projectid)
                .NotEmpty()
                .WithMessage("El id del proyecto es requerido");
        }
    }
}
