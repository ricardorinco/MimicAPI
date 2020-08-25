using Mimic.Application.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mimic.Application.Rules.Words.Update
{
    public class R00UpdateWordRules
    {
        public static Word ApplyRules(UpdateWordRequestDto request, Word word)
        {
            var rulesHandler = new List<IRuleHandler<UpdateWordRequestDto, Word>>();
            rulesHandler.Add(new R01UpdateWord());
            rulesHandler.Add(new R02AutoFillWord());

            foreach (var rule in rulesHandler)
            {
                rule.Next = rule;
            }

            return rulesHandler.FirstOrDefault().Apply(request, word);
        }
    }
}
