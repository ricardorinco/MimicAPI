using FluentValidation;
using FluentValidation.Results;
using Mimic.Application.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Application.Resources;

namespace Mimic.Application.Validations.Words.Update
{
    public class V02DataMustBeValid : AbstractValidator<UpdateWordRuleDto>, IValidationHandler<UpdateWordRuleDto>
    {
        public IValidationHandler<UpdateWordRuleDto> Next { get; set; }

        public ValidationResult Apply(UpdateWordRuleDto validationDto)
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
