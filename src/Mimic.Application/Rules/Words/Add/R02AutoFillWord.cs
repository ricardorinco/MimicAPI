using Mimic.Application.Arguments.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Domain.Models;
using System;

namespace Mimic.Application.Rules.Words.Add
{
    public class R02AutoFillWord : IRuleHandler<AddWordDto, Word>
    {
        public IRuleHandler<AddWordDto, Word> Next { get; set; }

        public Word Apply(AddWordDto ruleDto, Word word)
        {
            word.CreatedAt = DateTime.Now;
            word.Active = true;

            return word;
        }
    }
}
