using FluentValidation.Results;

namespace Mimic.Application.Interfaces
{
    public interface IValidationHandler<ValidationDto>
    {
        public IValidationHandler<ValidationDto> Next { get; set; }

        public ValidationResult Apply(ValidationDto validationDto);
    }
}
