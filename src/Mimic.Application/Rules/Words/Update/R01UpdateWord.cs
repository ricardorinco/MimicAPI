using Mimic.Application.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Domain.Models;
using System;

namespace Mimic.Application.Rules.Words.Update
{
    public class R01UpdateWord : IRuleHandler<UpdateWordRequestDto, Word>
    {
        public IRuleHandler<UpdateWordRequestDto, Word> Next { get; set; }

        public Word Apply(UpdateWordRequestDto request, Word foundWord)
        {
            if (request == null)
            {
                return null;
            }

            foundWord.Description = request.Description;
            foundWord.Points = request.Points;
            foundWord.Active = request.Active;
            foundWord.UpdatedAt = DateTime.Now;

            return foundWord;
        }
    }
}
