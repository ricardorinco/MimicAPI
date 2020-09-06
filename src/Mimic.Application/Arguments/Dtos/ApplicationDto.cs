using Mimic.Application.Arguments.Dtos.Validations;
using Mimic.Domain.Models;

namespace Mimic.Application.Arguments.Dtos
{
    public class ApplicationDto<EntityBase> where EntityBase : Base
    {
        public EntityBase Entity { get; private set; }
        public CustomValidation CustomValidation { get; private set; }

        public ApplicationDto(EntityBase entity)
        {
            Entity = entity;

            CustomValidation = new CustomValidation();
            CustomValidation.SetIsValid(true);
        }
        public ApplicationDto(CustomValidation customValidation)
        {
            CustomValidation = customValidation;
            CustomValidation.SetIsValid(false);
        }
    }
}
