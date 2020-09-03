using Mimic.Application.Interfaces;
using Mimic.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mimic.Application.Rules
{
    public static class ApplyRulesHandler<RuleDto, Entity>
    {
        private static List<IRuleHandler<RuleDto, Entity>> rulesHandler;

        public static Entity ApplyRules(RuleDto ruleDto, Entity entity, string nameSpace)
        {
            Reset();

            AddRulesToList(GetTypes(nameSpace));
            LinkNextRules();

            return rulesHandler.FirstOrDefault().Apply(ruleDto, entity);
        }

        private static Type[] GetTypes(string nameSpace)
        {
            return AssemblyUtil.GetTypesInNamespace(
                Assembly.GetExecutingAssembly(),
                $"{typeof(ApplyRulesHandler<RuleDto, Entity>).Namespace}.{nameSpace}"
            );
        }

        private static void Reset()
        {
            rulesHandler = new List<IRuleHandler<RuleDto, Entity>>();
        }
        private static void AddRulesToList(Type[] types)
        {
            foreach (var type in types)
            {
                var classRule = (IRuleHandler<RuleDto, Entity>)Activator.CreateInstance(type);
                rulesHandler.Add(classRule);
            }
        }
        private static void LinkNextRules()
        {
            int count = 1;
            foreach (var rule in rulesHandler)
            {
                if (count < rulesHandler.Count)
                {
                    rule.Next = rulesHandler[count];
                    count++;
                }
            }
        }
    }
}
