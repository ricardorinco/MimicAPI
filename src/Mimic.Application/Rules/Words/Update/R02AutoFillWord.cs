using Mimic.Application.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Domain.Models;
using System;

namespace Mimic.Application.Rules.Words.Update
{
    public class R02AutoFillWord : IRuleHandler<UpdateWordRuleDto, Word>
    {
        public IRuleHandler<UpdateWordRuleDto, Word> Next { get; set; }

        public Word Apply(UpdateWordRuleDto ruleDto, Word foundWord)
        {
            foundWord.UpdatedAt = DateTime.Now;

            return foundWord;
        }
    }
}
