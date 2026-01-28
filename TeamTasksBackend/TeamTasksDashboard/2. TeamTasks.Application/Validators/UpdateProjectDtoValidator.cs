using _3._TeamTasks.Domain.Dtos;
using FluentValidation;

namespace _2._TeamTasks.Application.Validators
{
    public class UpdateProjectDtoValidator : AbstractValidator<UpdateProjectDto>
    {
        public UpdateProjectDtoValidator()
        {
            RuleFor(x => x.Projectid)
                .NotEmpty()
                .WithMessage("El id del proyecto es requerido");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Se requiere el nombre del proyecto")
                .MaximumLength(150).WithMessage("La longitud máxima del nombre del proyecto es de 150 caracteres");

            RuleFor(x => x.Clientname)
                .NotEmpty().WithMessage("Se requiere el nombre del cliente")
                .MaximumLength(150).WithMessage("La longitud máxima del nombre del cliente es de 150 caracteres");

            RuleFor(x => x.Startdate)
                .NotEmpty().WithMessage("La fecha de inicio es requerida");

            RuleFor(x => x.Enddate)
                .GreaterThan(x => x.Startdate)
                .When(x => x.Enddate.HasValue)
                .WithMessage("La fecha de finalización debe ser mayor que la fecha de inicio");

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("El estado es requerido");
        }
    }
}
