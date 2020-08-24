using Mimic.Application.Dtos.Words;
using Mimic.Domain.Models;
using System;

namespace Mimic.Application.Rules.Words.Update
{
    public static class R01UpdateWord
    {
        public static Word Apply(UpdateWordRequestDto request, Word foundWord)
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
