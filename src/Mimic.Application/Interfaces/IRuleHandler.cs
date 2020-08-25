using Mimic.Domain.Models;

namespace Mimic.Application.Interfaces
{
    public interface IRuleHandler<EntityDto, Entity>
    {
        IRuleHandler<EntityDto, Entity> Next { get; set; }

        Word Apply(EntityDto entityDto, Entity entity);
    }
}
