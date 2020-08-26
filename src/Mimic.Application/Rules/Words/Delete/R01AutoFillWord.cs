using Mimic.Application.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Domain.Models;
using System;

namespace Mimic.Application.Rules.Words.Delete
{
    public class R01AutoFillWord : IRuleHandler<DeleteWordRuleDto, Word>
    {
        public IRuleHandler<DeleteWordRuleDto, Word> Next { get; set; }

        public Word Apply(DeleteWordRuleDto ruleDto, Word foundWord)
        {
            foundWord.Active = false;
            foundWord.UpdatedAt = DateTime.Now;

            return foundWord;
        }
    }
}
