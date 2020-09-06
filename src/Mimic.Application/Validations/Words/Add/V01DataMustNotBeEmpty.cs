using FluentValidation;
using FluentValidation.Results;
using Mimic.Application.Arguments.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Application.Resources;

namespace Mimic.Application.Validations.Words.Add
{
    public class V01DataMustNotBeEmpty : AbstractValidator<AddWordRuleDto>, IValidationHandler<AddWordRuleDto>
    {
        public IValidationHandler<AddWordRuleDto> Next { get; set; }

        public ValidationResult Apply(AddWordRuleDto validationDto)
        {
            RuleFor(word => word.Description)
               .NotEmpty()
               .WithMessage(
                   string.Format(ValidationsResource.CannotBeEmpty, "Description")
               );

            RuleFor(word => word.Points)
                .NotNull()
                .WithMessage(
                    string.Format(ValidationsResource.CannotBeEmpty, "Points")
                );

            return Validate(validationDto);
        }
    }
}
