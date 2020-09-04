using FluentValidation.Results;
using System.Collections.Generic;

namespace Mimic.Application.Dtos.Validations
{
    public class CustomValidation
    {
        public List<Error> Errors { get; private set; }

        public bool IsValid { get; private set; }

        public CustomValidation()
        {
            Errors = new List<Error>();
            IsValid = true;
        }

        public void AddError(IList<ValidationFailure> validationsFailure)
        {
            foreach (var validationFailure in validationsFailure)
            {
                AddError(validationFailure);
            }
        }
        public void AddError(ValidationFailure validationFailure)
        {
            Errors.Add(new Error
            {
                PropertyName = validationFailure.PropertyName,
                Message = validationFailure.ErrorMessage,
                ErrorCode = validationFailure.ErrorCode
            });
        }
        public void AddError(string propertyName, string errorMessage, string errorCode)
        {
            Errors.Add(new Error
            {
                PropertyName = propertyName,
                Message = errorMessage,
                ErrorCode = errorCode
            });
        }

        public void SetIsValid(bool isValid)
        {
            if (!isValid)
            {
                IsValid = isValid;
            }
        }
    }
}
