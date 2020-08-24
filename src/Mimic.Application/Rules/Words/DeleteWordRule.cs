using Mimic.Domain.Models;
using System;

namespace Mimic.Application.Rules.Words
{
    public static class DeleteWordRule
    {
        public static Word Apply(Word foundWord)
        {
            foundWord.Active = false;
            foundWord.UpdatedAt = DateTime.Now;

            return foundWord;
        }
    }
}
