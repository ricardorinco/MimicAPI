using Mimic.Application.Interfaces;
using Mimic.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mimic.Application.Rules
{
    public static class ApplyRulesHandler<EntityDto, Entity>
    {
        private static List<IRuleHandler<EntityDto, Entity>> rulesHandler;

        public static Entity ApplyRules(EntityDto entityDto, Entity entity, string nameSpace)
        {
            rulesHandler = new List<IRuleHandler<EntityDto, Entity>>();

            var types = AssemblyUtil.GetTypesInNamespace(
                Assembly.GetExecutingAssembly(),
                $"{typeof(ApplyRulesHandler<EntityDto, Entity>).Namespace}.{nameSpace}"
            );

            foreach (var type in types)
            {
                var classRule = (IRuleHandler<EntityDto, Entity>)Activator.CreateInstance(type);
                rulesHandler.Add(classRule);
            }

            int count = 1;
            foreach (var rule in rulesHandler)
            {
                if (count < rulesHandler.Count)
                {
                    rule.Next = rulesHandler[count];
                    count++;
                }
            }

            return rulesHandler.FirstOrDefault().Apply(entityDto, entity);
        }
    }
}
