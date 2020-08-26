using Mimic.Application.Interfaces;
using Mimic.Application.Utils;
using Mimic.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mimic.Application.Rules
{
    public static class ApplyRulesHandler<EntityDto, Entity>
    {
        private static List<IRuleHandler<EntityDto, Entity>> rulesHandler = new List<IRuleHandler<EntityDto, Entity>>();

        public static Word ApplyRules(EntityDto entityDto, Entity entity, string nameSpace)
        {
            var types = AssemblyUtil.GetTypesInNamespace(
                Assembly.GetExecutingAssembly(),
                $"{typeof(ApplyRulesHandler<EntityDto, Entity>).Namespace}.{nameSpace}"
            );

            foreach (var type in types)
            {
                var classRule = (IRuleHandler<EntityDto, Entity>)Activator.CreateInstance(type);
                rulesHandler.Add(classRule);

                classRule.Next = classRule;
            }

            return rulesHandler.FirstOrDefault().Apply(entityDto, entity);
        }
    }
}
