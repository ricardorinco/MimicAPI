using Mimic.Domain.Models;
using System;

namespace Mimic.Application.Rules.Words.Update
{
    public static class R02AutoFillWord
    {
        public static Word Apply(Word word)
        {
            if (word == null)
            {
                return null;
            }

            word.UpdatedAt = DateTime.Now;

            return word;
        }
    }
}
