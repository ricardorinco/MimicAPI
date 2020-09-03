namespace Mimic.Application.Interfaces
{
    public interface IValidationHandler<RuleDto>
    {
        IValidationHandler<RuleDto> Next { get; set; }

        dynamic Apply(RuleDto ruleDto);
    }
}
