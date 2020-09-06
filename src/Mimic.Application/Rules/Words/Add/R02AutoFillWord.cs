using Mimic.Application.Arguments.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Domain.Models;
using System;

namespace Mimic.Application.Rules.Words.Add
{
    public class R02AutoFillWord : IRuleHandler<AddWordRuleDto, Word>
    {
        public IRuleHandler<AddWordRuleDto, Word> Next { get; set; }

        public Word Apply(AddWordRuleDto ruleDto, Word word)
        {
            word.CreatedAt = DateTime.Now;
            word.Active = true;

            return word;
        }
    }
}
