using Mimic.Application.Dtos.Validations;
using Mimic.Domain.Models;

namespace Mimic.Application.Dtos
{
    public class ApplicationDto<EntityBase> where EntityBase : Base
    {
        public EntityBase Entity { get; private set; }
        public CustomValidation CustomValidation { get; private set; }

        public ApplicationDto(EntityBase entity)
        {
            Entity = entity;
        }

        public ApplicationDto(CustomValidation customValidation)
        {
            CustomValidation = customValidation;
        }
    }
}
