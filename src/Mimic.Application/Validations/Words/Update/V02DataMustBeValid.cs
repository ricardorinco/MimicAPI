using FluentValidation;
using FluentValidation.Results;
using Mimic.Application.Arguments.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Application.Resources;

namespace Mimic.Application.Validations.Words.Update
{
    public class V02DataMustBeValid : AbstractValidator<UpdateWordDto>, IValidationHandler<UpdateWordDto>
    {
        public IValidationHandler<UpdateWordDto> Next { get; set; }

        public ValidationResult Apply(UpdateWordDto validationDto)
        {
            RuleFor(word => word.Description)
                .MaximumLength(120)
                .WithMessage(
                    string.Format(ValidationsResource.MaximumCharacterLimit, "Description")
                );

            RuleFor(word => word.Points)
                .GreaterThan(0)
                .WithMessage(
                    string.Format(ValidationsResource.GreaterThanZero, "Points")
                );

            return Validate(validationDto);
        }
    }
}
