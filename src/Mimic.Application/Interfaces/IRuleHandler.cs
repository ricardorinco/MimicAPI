namespace Mimic.Application.Interfaces
{
    public interface IRuleHandler<RuleDto, Entity>
    {
        IRuleHandler<RuleDto, Entity> Next { get; set; }

        Entity Apply(RuleDto ruleDto, Entity entity);
    }
}
