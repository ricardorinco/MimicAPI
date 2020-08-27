namespace Mimic.Application.Interfaces
{
    public interface IRuleHandler<EntityDto, Entity>
    {
        IRuleHandler<EntityDto, Entity> Next { get; set; }

        Entity Apply(EntityDto entityDto, Entity entity);
    }
}
