using _3._TeamTasks.Domain.Dtos;
using FluentValidation;

namespace _3._TeamTasks.Domain.Validators
{
    public class UpdateDeveloperDtoValidator : AbstractValidator<UpdateDeveloperDto>
    {
        public UpdateDeveloperDtoValidator()
        {
            RuleFor(x => x.Developerid)
                .NotEmpty()
                .WithMessage("El id del desarrollador es requerido");

            RuleFor(x => x.Firstname)
                .NotEmpty().WithMessage("Se requiere el nombre")
                .MaximumLength(100).WithMessage("La longitud máxima del nombre es de 100 caracteres");

            RuleFor(x => x.Lastname)
                .NotEmpty().WithMessage("Se requiere el apellido")
                .MaximumLength(100).WithMessage("La longitud máxima del apellido es de 100 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Se rquiere el correo")
                .EmailAddress().WithMessage("Formato de correo electrónico no válido");

            RuleFor(x => x.Isactive)
                .NotNull().WithMessage("El estado activo/inactivo es requerido");
        }
    }
}
