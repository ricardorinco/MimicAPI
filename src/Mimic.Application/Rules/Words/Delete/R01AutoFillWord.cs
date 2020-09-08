using Mimic.Application.Arguments.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Domain.Models;
using System;

namespace Mimic.Application.Rules.Words.Delete
{
    public class R01AutoFillWord : IRuleHandler<DeleteWordDto, Word>
    {
        public IRuleHandler<DeleteWordDto, Word> Next { get; set; }

        public Word Apply(DeleteWordDto ruleDto, Word foundWord)
        {
            foundWord.Active = false;
            foundWord.UpdatedAt = DateTime.Now;

            return foundWord;
        }
    }
}
