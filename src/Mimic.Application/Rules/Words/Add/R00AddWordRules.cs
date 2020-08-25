using Mimic.Application.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mimic.Application.Rules.Words.Add
{
    public class R00AddWordRules
    {
        public static Word ApplyRules(AddWordRuleDto ruleDto, Word word)
        {
            var rulesHandler = new List<IRuleHandler<AddWordRuleDto, Word>>();
            rulesHandler.Add(new R01CreateWord());
            rulesHandler.Add(new R02AutoFillWord());

            foreach (var rule in rulesHandler)
            {
                rule.Next = rule;
            }

            return rulesHandler.FirstOrDefault().Apply(ruleDto, word);
        }
    }
}
