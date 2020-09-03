using FluentValidation;
using FluentValidation.Results;
using Mimic.Application.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Application.Resources;

namespace Mimic.Application.Validations.Words.Add
{
    public class V02DataMustBeValid : AbstractValidator<AddWordRuleDto>, IValidationHandler<AddWordRuleDto>
    {
        public IValidationHandler<AddWordRuleDto> Next { get; set; }

        public ValidationResult Apply(AddWordRuleDto validationDto)
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
