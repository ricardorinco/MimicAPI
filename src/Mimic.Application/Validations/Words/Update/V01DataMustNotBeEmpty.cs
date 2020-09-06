using FluentValidation;
using FluentValidation.Results;
using Mimic.Application.Arguments.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Application.Resources;

namespace Mimic.Application.Validations.Words.Update
{
    public class V01DataMustNotBeEmpty : AbstractValidator<UpdateWordRuleDto>, IValidationHandler<UpdateWordRuleDto>
    {
        public IValidationHandler<UpdateWordRuleDto> Next { get; set; }

        public ValidationResult Apply(UpdateWordRuleDto validationDto)
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
