using Mimic.Application.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Domain.Models;
using System;

namespace Mimic.Application.Rules.Words.Update
{
    public class R02AutoFillWord : IRuleHandler<UpdateWordRequestDto, Word>
    {
        public IRuleHandler<UpdateWordRequestDto, Word> Next { get; set; }

        public Word Apply(UpdateWordRequestDto request, Word foundWord)
        {
            if (foundWord == null)
            {
                return null;
            }

            foundWord.UpdatedAt = DateTime.Now;

            return foundWord;
        }
    }
}
