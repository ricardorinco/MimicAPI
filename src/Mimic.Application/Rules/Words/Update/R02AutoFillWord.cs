using Mimic.Application.Arguments.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Domain.Models;
using System;

namespace Mimic.Application.Rules.Words.Update
{
    public class R02AutoFillWord : IRuleHandler<UpdateWordDto, Word>
    {
        public IRuleHandler<UpdateWordDto, Word> Next { get; set; }

        public Word Apply(UpdateWordDto ruleDto, Word foundWord)
        {
            foundWord.UpdatedAt = DateTime.Now;

            return foundWord;
        }
    }
}
