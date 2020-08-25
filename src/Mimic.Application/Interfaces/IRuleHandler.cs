using Mimic.Domain.Models;

namespace Mimic.Application.Interfaces
{
    public interface IRuleHandler<Request, Entity>
    {
        IRuleHandler<Request, Entity> Next { get; set; }

        Word Apply(Request request, Entity entity);
    }
}
