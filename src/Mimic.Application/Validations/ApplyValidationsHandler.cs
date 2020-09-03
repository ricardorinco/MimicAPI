using Mimic.Application.Dtos.Validations;
using Mimic.Application.Interfaces;
using Mimic.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimic.Application.Validations
{
    public class ApplyValidationsHandler<ValidationDto>
    {
        private static List<IValidationHandler<ValidationDto>> validationsHandler;
        private static CustomValidation customValidation;

        public static CustomValidation ApplyRules(ValidationDto validationDto, string nameSpace)
        {
            Reset();

            AddValidationsToList(nameSpace);
            LinkNextValidations(validationDto);

            validationsHandler.FirstOrDefault();

            return customValidation;
        }

        private static void Reset()
        {
            validationsHandler = new List<IValidationHandler<ValidationDto>>();
            customValidation = new CustomValidation();
        }
        private static void AddValidationsToList(string nameSpace)
        {
            var types = AssemblyUtil.GetTypes<ApplyValidationsHandler<ValidationDto>>(nameSpace);

            foreach (var type in types)
            {
                var classValidation = (IValidationHandler<ValidationDto>)Activator.CreateInstance(type);
                validationsHandler.Add(classValidation);
            }
        }
        private static void LinkNextValidations(ValidationDto validationDto)
        {
            int count = 1;
            foreach (var validation in validationsHandler)
            {
                if (count < validationsHandler.Count)
                {
                    var validationResult = validation.Apply(validationDto);
                    customValidation.AddError(validationResult.Errors);
                    customValidation.SetIsValid(validationResult.IsValid);

                    validation.Next = validationsHandler[count];
                    count++;
                }
            }
        }
    }
}
